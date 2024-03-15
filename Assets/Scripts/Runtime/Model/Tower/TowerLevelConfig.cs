using System;
using System.Collections.Generic;
using UnityEngine;

namespace MicroTowerDefence
{
    [CreateAssetMenu]
    public class TowerLevelConfig : ScriptableObject
    {
        public TowerType Tower;
        public ElementType Element;
        [Range(30, 200)] public uint CostToBuild;
        [SerializeField] private TowerConfig _level1, _level2, _level3;

        private Dictionary<uint, TowerConfig> _configs;

        public TowerConfig Get(uint level)
        {
            _configs = new()
            {
                {0, _level1 },
                {1, _level2 },
                {2, _level3 }
            };

            if (_configs.TryGetValue(level, out var config))
            {
                config.Init(Tower, Element);
                return config;
            }

            return null;
        }
    }

    [Serializable]
    public class TowerConfig
    {
        public TowerBase Prefab;
        [NonSerialized] public TowerType Tower;
        [NonSerialized] public ElementType Element;
        [Range(1f, 5f)] public float TargetRange;
        [Range(10, 100)] public int Damage;
        [Range(30, 300)] public uint UpgradeCost;
        [Range(20, 300)] public uint SellCost;
        [Range(0.1f, 1f)] public float ShellBlastRadius;
        [Range(0.2f, 3f)] public float ShootPerSecond;
        [Range(1, 100)] public int DamagePerSecond;

        public void Init(TowerType tower, ElementType element)
        {
            Tower = tower;
            Element = element;
        }
    }
}