using UnityEngine;

public class TowerFactory : GameObjectFactory
{
    [SerializeField] private TowerBase[] _beamPrefabs;
    [SerializeField] private TowerBase[] _mortarPrefabs;
    [SerializeField] private TowerBase[] _archerPrefabs;
    [SerializeField] private TowerBase[] _magicPrefabs;
    [Space]
    [SerializeField] private TowerLevelConfig _towerLevelConfig;

    public TowerBase Get(TowerType type, int level = 0)
    {
        return type switch
        {
            TowerType.Beam => Get(_beamPrefabs[level], level),
            TowerType.Mortar => Get(_mortarPrefabs[level], level),
            TowerType.Archer => Get(_archerPrefabs[level], level),
            TowerType.Magic => Get(_magicPrefabs[level], level),
            _ => null,
        };
    }

    private T Get<T>(T prefab, int level = 0) where T : TowerBase
    {
        T instance = CreateGameObjectInstance(prefab);
        instance.Initialize(GetConfig(level), level);
        return instance;
    }

    private TowerConfig GetConfig(int level)
    {
        return _towerLevelConfig.Get(level);
    }
}

public enum TowerType : byte
{
    Beam = 101,
    Mortar = 102,
    Archer = 103,
    Magic = 104,
}
