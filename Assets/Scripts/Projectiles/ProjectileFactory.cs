using System.Collections;
using System.Collections.Generic;
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

    private  T Get<T>(T prefab) where T : Projectile
    {
        T instanle = CreateGameObjectInstance(prefab);
        instanle.OriginFactory = this;
        return instanle;
    }

    public void Reclaim(Projectile entity)
    {
        Destroy(entity.gameObject);
    }
}
