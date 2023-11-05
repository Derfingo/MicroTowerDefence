using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class GameTileContentFactory : GameObjectFactory
{
    [SerializeField] private GameTileContent _destinationPrefab;
    [SerializeField] private GameTileContent _emptyPrefab;
    [SerializeField] private GameTileContent _wallPrefab;
    [SerializeField] private GameTileContent _spawnPrefab;
    [SerializeField] private GameTileContent _placePrefab;
    [Space]
    [SerializeField] private Tower _laserTowerPrefab;
    [SerializeField] private Tower _ballistaTowerPrefab;

    public void Reclaim(GameTileContent content)
    {
        Destroy(content.gameObject);
    }

    public GameTileContent Get(GameTileContentType contentType)
    {
        return contentType switch
        {
            GameTileContentType.Destination => Get(_destinationPrefab),
            GameTileContentType.Empty => Get(_emptyPrefab),
            GameTileContentType.Wall => Get(_wallPrefab),
            GameTileContentType.Spawn => Get(_spawnPrefab),
            GameTileContentType.Place => Get(_placePrefab),
            GameTileContentType.Ballista => Get(_ballistaTowerPrefab),
            GameTileContentType.Laser => Get(_laserTowerPrefab),
            _ => null,
        };
    }

    private T Get<T>(T prefab) where T : GameTileContent
    {
        T instance = CreateGameObjectInstance(prefab);
        instance.OriginFactory = this;
        return instance;
    }
}
