using System;
using UnityEngine;

[CreateAssetMenu]
public class TowerLevelConfig : ScriptableObject
{
    [SerializeField] private TowerConfig _firstPrefab;
    [SerializeField] private TowerConfig _secondPrefab;
    [SerializeField] private TowerConfig _thirdPrefab;

    [Serializable]
    public class TowerConfig
    {
        [Range(1f, 5f)] public float TargetTange;
        [Range(50, 300)] public int Cost;
    }

    public TowerConfig GetConfig(TowerLevel level)
    {
        switch (level)
        {
            case TowerLevel.First: return _firstPrefab;
            case TowerLevel.Second: return _secondPrefab;
            case TowerLevel.Third: return _thirdPrefab;
        }

        Debug.Log($"No config for {level}");
        return _firstPrefab;
    }
}

public enum TowerLevel : byte
{
    First,
    Second,
    Third
}
