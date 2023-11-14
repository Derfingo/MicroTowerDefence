using UnityEngine;

[CreateAssetMenu]
public class TileContentFactory : GameObjectFactory
{
    [Header("Content")]
    [SerializeField] private TileContent _destinationPrefab;
    [SerializeField] private TileContent _emptyPrefab;
    [SerializeField] private TileContent _wallPrefab;
    [SerializeField] private TileContent _spawnPrefab;
    [SerializeField] private TileContent _placePrefab;
    [Space]
    [Header("Towers")]
    [SerializeField] private Tower[] _beamPrefabs;
    [SerializeField] private Tower[] _mortarPrefabs;
    [SerializeField] private Tower[] _archerPrefabs;
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

    public TileContent Get(TileContentType contentType, int level = 0)
    {
        return contentType switch
        {
            TileContentType.Destination => Get(_destinationPrefab, level),
            TileContentType.Empty => Get(_emptyPrefab, level),
            TileContentType.Spawn => Get(_spawnPrefab, level),
            TileContentType.Place => Get(_placePrefab, level),
            TileContentType.Wall => Get(_wallPrefab, level),
            _ => null,
        };
    }

    public TileContent Get(TowerType type, int level = 0)
    {
        return type switch
        {
            TowerType.Beam => Get(_beamPrefabs[level], level),
            TowerType.Mortar => Get(_mortarPrefabs[level], level),
            TowerType.Archer => Get(_archerPrefabs[level], level),
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
