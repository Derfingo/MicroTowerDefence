using System;
using UnityEngine;

public class LevelConfigProvider : ScriptableObject, ILevelConfig
{
    [SerializeField] private LevelConfig _levelConfig;

    [Serializable]
    public class LevelConfig
    {
        public int Level;
        public string Name;
        public string Description;
    }
}
