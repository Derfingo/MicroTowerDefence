using UnityEngine;

[CreateAssetMenu]
public class ProjectileFactory : GameObjectFactory
{
    [SerializeField] private Shell _shellPrefab;
    [SerializeField] private Explosion _explosionPrefab;
    [SerializeField] private Arrow _arrowPrefab;
    [SerializeField] private MagicSphere _magicSpherePrefab;

    public Shell Shell => Get(_shellPrefab);
    public Arrow Arrow => Get(_arrowPrefab);
    public MagicSphere MagicSphere => Get(_magicSpherePrefab);
    public Explosion Explosion => Instantiate(_explosionPrefab);

    public ProjectileBase Get(ProjectileType type)
    {
        return type switch
        {
            ProjectileType.Shell => Get(_shellPrefab),
            ProjectileType.Arrow => Get(_arrowPrefab),
            ProjectileType.Sphere => Get(_magicSpherePrefab),
            _ => null,
        };
    }

    private T Get<T>(T prefab) where T : ProjectileBase
    {
        T instanle = CreateGameObjectInstance(prefab);
        return instanle;
    }

    public void Reclaim(ProjectileBase entity, float delay = 0f)
    {
        Destroy(entity.gameObject, delay);
    }
}

public enum ProjectileType
{
    Arrow,
    Shell,
    Explosion,
    Sphere,
    Test
}