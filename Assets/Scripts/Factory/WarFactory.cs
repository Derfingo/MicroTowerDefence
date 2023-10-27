using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WarFactory : GameObjectFactory
{
    [SerializeField] private Shell _shellPrefab;
    [SerializeField] private Explosion _explosionPrefab;

    public Shell Shell => Get(_shellPrefab);
    public Explosion Explosion => Get(_explosionPrefab);

    private T Get<T>(T prefab) where T : WarEntity
    {
        T instanle = CreateGameObjectInstance(prefab);
        instanle.OriginFactory = this;
        return instanle;
    }

    public void Reclaim(WarEntity entity)
    {
        Destroy(entity.gameObject);
    }
}
