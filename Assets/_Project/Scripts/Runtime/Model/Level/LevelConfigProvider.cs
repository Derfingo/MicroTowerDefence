using System;
using UnityEngine;

namespace MicroTowerDefence
{
    [CreateAssetMenu]
    public class LevelConfigProvider : ScriptableObject
    {
        [SerializeField] private LevelConfig _levelConfig;

        public LevelConfig Get()
        {
            return _levelConfig;
        }
    }

    [Serializable]
    public class LevelConfig
    {
        public sbyte Level;
        public string Name;
        public string Description;
        [Range(0f, 60f)] public float PrepareTime = 5f;
        [Range(50, 3000)] public uint Coins = 100;
        [Range(1, 20)] public uint Health = 10;
    }
}