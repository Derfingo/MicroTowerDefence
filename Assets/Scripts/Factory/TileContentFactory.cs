using UnityEngine;

[CreateAssetMenu]
public class TileContentFactory : GameObjectFactory
{
    [Header("Content")]
    [Space]
    [Header("Towers")]
    [SerializeField] private TowerBase[] _beamPrefabs;
    [SerializeField] private TowerBase[] _mortarPrefabs;
    [SerializeField] private TowerBase[] _archerPrefabs;
    [SerializeField] private TowerBase[] _magicPrefabs;
    [Space]
    [SerializeField] private TowerLevelConfig _towerLevelConfig;

    public void Reclaim(TileContent content)
    {
        Destroy(content.gameObject);
    }

    public TowerConfig GetConfig(int level)
    {
        return _towerLevelConfig.Get(level);
    }

    public TileContent Get(TowerType type, int level = 0)
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

    private T Get<T>(T prefab, int level) where T : TileContent
    {
        T instance = CreateGameObjectInstance(prefab);
        instance.Initialize(this, level);
        return instance;
    }
}
