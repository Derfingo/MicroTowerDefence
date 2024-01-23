using System;
using UnityEngine;

public class ViewConfigProvider : ScriptableObject, ILevelConfig
{
    [SerializeField] private TowerFactory _towerFactory;

    [Serializable]
    public class ViewConfig
    {

    }
}
