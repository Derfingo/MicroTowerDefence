using System;
using UnityEngine;

[CreateAssetMenu]
public class TowerLevelConfig : ScriptableObject
{
    [SerializeField] private TowerConfig _level1, _level2, _level3;

    public TowerConfig Get(int level)
    {
        return level switch
        {
            0 => _level1,
            1 => _level2,
            2 => _level3,
            _ => null
        };
    }
}

[Serializable]
public class TowerConfig
{
    [Range(1f, 5f)] public float TargetRange;
    [Range(10, 100)] public int Damage;
    [Range(50, 300)] public int Cost;
    [Range(0.1f, 1f)] public float ShellBlastRadius;
    [Range(0.2f, 3f)] public float ShootPerSecond;
    [Range(1f, 100f)] public float DamagePerSecond;
}
