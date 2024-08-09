using System;
using UnityEngine;

namespace MicroTowerDefence
{
    [CreateAssetMenu]
    public class EnemyFactory : GameObjectFactory
    {
        [SerializeField] private EnemyConfig[] _enemies;

        private EnemyConfig GetConfig(EnemyType type)
        {
            foreach (var config in _enemies)
            {
                if (config.Enemy == type)
                {
                    return config;
                }
            }

            Debug.LogError($"No config for {type}");
            return null;
        }

        public EnemyBase Get(EnemyType type)
        {
            var config = GetConfig(type);
            EnemyBase instance = CreateGameObjectInstance(config.Prefab);
            instance.OriginFactory = this;
            instance.Initialize(config);
            return instance;
        }

        public void Reclaim(EnemyBase enemy)
        {
            Destroy(enemy.gameObject);
        }
    }

    [Serializable]
    public class EnemyConfig
    {
        public EnemyBase Prefab;
        public EnemyType Enemy;
        public ElementType Element;
        [Range(0.5f, 2f)] public float Scale = 1f;
        [Range(-0.5f, 0.5f)] public float PathOffset = 0f;
        [Range(0.2f, 2f)] public float Speed = 1f;
        [Range(10f, 1000f)] public float Health = 100f;
        [Range(0f, 1000f)] public float Shield = 100f;
        [Range(10, 100)] public uint Coins = 10;
        [Range(1, 5)] public uint Damage = 1;
    }
}