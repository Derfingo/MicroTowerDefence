using UnityEngine;

[CreateAssetMenu]
public class ProjectileFactory : GameObjectFactory
{
    [SerializeField] private Shell _shellPrefab;
    [SerializeField] private Explosion _explosionPrefab;
    [SerializeField] private Arrow _arrowPrefab;
    [SerializeField] private MagicSphere _magicSpherePrefab;

    public Shell Shell => Get(_shellPrefab);
    public Explosion Explosion => Get(_explosionPrefab);
    public Arrow Arrow => Get(_arrowPrefab);
    public MagicSphere MagicSphere => Get(_magicSpherePrefab);

    public GameBehaviour Get(ProjectileType type)
    {
        return type switch
        {
            ProjectileType.Shell => Get(_shellPrefab),
            ProjectileType.Explosion => Get(_explosionPrefab),
            ProjectileType.Arrow => Get(_arrowPrefab),
            ProjectileType.Sphere => Get(_magicSpherePrefab),
            _ => null,
        };
    }

    private  T Get<T>(T prefab) where T : GameBehaviour
    {
        T instanle = CreateGameObjectInstance(prefab);
        instanle.OriginFactory = this;
        return instanle;
    }

    public void Reclaim(GameBehaviour entity, float delay = 0f)
    {
        Destroy(entity.gameObject, delay);
    }
}