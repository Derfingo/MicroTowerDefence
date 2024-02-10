using UnityEngine;

namespace MicroTowerDefence
{
    [CreateAssetMenu]
    public class ProjectileFactory : GameObjectFactory
    {
        [SerializeField] private Shell[] _shellPrefab;
        [SerializeField] private Arrow[] _arrowPrefab;
        [SerializeField] private Explosion _explosionPrefab;
        [SerializeField] private MagicSphere[] _magicSpherePrefabs;

        public Shell Shell => Get(_shellPrefab[0]);
        public Arrow Arrow => Get(_arrowPrefab[0]);
        public Explosion Explosion => Instantiate(_explosionPrefab);

        public ProjectileBase Get(ProjectileType type, uint level)
        {
            return type switch
            {
                ProjectileType.Shell => Get(_shellPrefab[level]),
                ProjectileType.Arrow => Get(_arrowPrefab[level]),
                ProjectileType.Sphere => Get(_magicSpherePrefabs[level]),
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
}