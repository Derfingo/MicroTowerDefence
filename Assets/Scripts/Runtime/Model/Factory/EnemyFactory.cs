using System;
using UnityEngine;

public class EnemyFactory : GameObjectFactory
{
    [SerializeField] private EnemyConfig _small, _medium, _large;

    [Serializable]
    public class EnemyConfig
    {
        public Enemy Prefab;
        [FloatRangeSlider(0.5f,2f)] public FloatRange Scale = new(1f);
        [FloatRangeSlider(-0.4f, 0.4f)] public FloatRange PathOffset = new(0f);
        [FloatRangeSlider(0.2f, 5f)] public FloatRange Speed = new(1f);
        [FloatRangeSlider(10f, 1000f)] public FloatRange Health = new(100f);
        [SerializeField, Range(10, 50)] public uint Coins = 10;
        [SerializeField, Range(1, 5)] public uint Damage = 1;
    }

    private EnemyConfig GetConfig(EnemyType type)
    {
        switch (type)
        {
            case EnemyType.Large: return _large;
            case EnemyType.Medium: return _medium;
            case EnemyType.Small: return _small;
            case EnemyType.Slime : return _medium;
        }

        Debug.LogError($"No config for {type}");
        return _medium;
    }

    public Enemy Get(EnemyType type)
    {
        var config = GetConfig(type);
        Enemy instance = CreateGameObjectInstance(config.Prefab);
        instance.OriginFactory = this;
        instance.Initialize(config);
        return instance;
    }

    public void Reclaim(Enemy enemy)
    {
        Destroy(enemy.gameObject);
    }
}