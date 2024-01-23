using UnityEngine;

public class TowerFactory : GameObjectFactory
{
    [SerializeField] private TowerLevelConfig _beamConfig;
    [SerializeField] private TowerLevelConfig _mortarConfig;
    [SerializeField] private TowerLevelConfig _archerConfig;
    [SerializeField] private TowerLevelConfig _magicConfig;

    public TowerBase Get(TowerType type, int level = 0)
    {
        TowerLevelConfig config = GetConfig(type);
        TowerConfig tower = config.Get(level);
        TowerBase instance = CreateGameObjectInstance(tower.Prefab);
        instance.Initialize(config.Get(level), level);
        return instance;
    }

    public uint GetCost(TowerType type)
    {
        return type switch
        {
            TowerType.Beam => 0,
            TowerType.Mortar => 0,
            TowerType.Archer => 0,
            TowerType.Magic => 0,
            _ => throw new System.NotImplementedException(),
        };
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

public enum TowerType : byte
{
    Beam = 101,
    Mortar = 102,
    Archer = 103,
    Magic = 104,
}