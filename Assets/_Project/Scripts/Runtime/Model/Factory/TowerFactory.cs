using System;
using System.Collections.Generic;
using UnityEngine;

namespace MicroTowerDefence
{
    public class TowerFactory : GameObjectFactory
    {
        [SerializeField] private TowerLevelConfig _beamConfig;
        [SerializeField] private TowerLevelConfig _mortarConfig;
        [SerializeField] private TowerLevelConfig _archerConfig;
        [SerializeField] private TowerLevelConfig _magicConfig;

        public TowerBase Get(TowerType type, uint level = 0)
        {
            TowerLevelConfig config = GetConfig(type);
            TowerConfig tower = config.Get(level);
            TowerBase instance = CreateGameObjectInstance(tower.Prefab);
            instance.Initialize(config.Get(level), level);
            return instance;
        }

        public uint GetCostToBuild(TowerType type)
        {
            return type switch
            {
                TowerType.Beam => GetConfig(type).CostToBuild,
                TowerType.Mortar => GetConfig(type).CostToBuild,
                TowerType.Archer => GetConfig(type).CostToBuild,
                TowerType.Magic => GetConfig(type).CostToBuild,
                _ => throw new NotImplementedException()
            };
        }

        public Dictionary<TowerType, uint> GetAllCostTowers()
        {
            var dictionary = new Dictionary<TowerType, uint>();

            foreach (TowerType type in Enum.GetValues(typeof(TowerType)))
            {
                dictionary.Add(type, GetCostToBuild(type));
            }

            return dictionary;
        }

        public TowerLevelConfig GetConfig(TowerType type)
        {
            return type switch
            {
                TowerType.Mortar => _mortarConfig,
                TowerType.Archer => _archerConfig,
                TowerType.Magic => _magicConfig,
                TowerType.Beam => _beamConfig,
                _ => null
            };
        }
    }
}