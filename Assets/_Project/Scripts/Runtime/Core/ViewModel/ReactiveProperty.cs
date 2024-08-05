using System;
using System.Collections.Generic;
using UnityEngine;
//using UniRx.InternalUtil;

namespace MicroTowerDefence
{
    public class ReactiveProperty<T> : IReactiveProperty<T>
    {
        public event Action<T> OnChangeEvent = delegate { };
        public bool IsEveryUpdatable;

        private static readonly IEqualityComparer<T> _comparer = EqualityComparer<T>.Default;
        private T _value = default;

        public T Value
        {
            get => _value;
            set
            {
                if (IsEveryUpdatable)
                {
                    SetValue(value);
                }
                else if (_comparer.Equals(value, _value) == false)
                {
                    SetValue(value);
                }
            }
        }

        private void SetValue(T value)
        {
            _value = value;
            OnChangeEvent?.Invoke(_value);
        }
    }
}
