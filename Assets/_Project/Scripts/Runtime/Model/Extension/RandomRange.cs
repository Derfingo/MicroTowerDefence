using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MicroTowerDefence
{
    [Serializable]
    public struct RandomRange
    {
        [SerializeField] private float _min, _max;

        public readonly float Min => _min;
        public readonly float Max => _max;

        public float RandomValueInRange
        {
            get => Random.Range(_min, _max);
        }

        public RandomRange(float value)
        {
            _min = _max = value;
        }

        public RandomRange(float min, float max)
        {
            _min = min;
            _max = max;
        }
    }

    public class FloatRangeSliderAttribute : PropertyAttribute
    {
        public float Min { get; private set; }
        public float Max { get; private set; }

        public FloatRangeSliderAttribute(float min, float max)
        {
            Min = min;
            Max = max < min ? min : max;
        }
    }
}
