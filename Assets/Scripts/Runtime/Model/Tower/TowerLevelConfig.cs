using MicroTowerDefence;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MicroTowerDefence
{
    [CreateAssetMenu]
    public class TowerLevelConfig : ScriptableObject
    {
        public TowerType Type;
        [Range(30, 200)] public uint CostToBuild;
        [SerializeField] private TowerConfig _level1, _level2, _level3;

        public TowerConfig Get(uint level)
        {
            return level switch
            {
                0 => _level1,
                1 => _level2,
                2 => _level3,
                _ => null
            };
        }
    }

    [Serializable]
    public class TowerConfig
    {
        public TowerBase Prefab;

        [Range(1f, 5f)] public float TargetRange;
        [Range(10, 100)] public int Damage;
        [Range(30, 300)] public uint UpgradeCost;
        [Range(20, 300)] public uint SellCost;
        [Range(0.1f, 1f)] public float ShellBlastRadius;
        [Range(0.2f, 3f)] public float ShootPerSecond;
        [Range(1f, 100f)] public float DamagePerSecond;
    }
}