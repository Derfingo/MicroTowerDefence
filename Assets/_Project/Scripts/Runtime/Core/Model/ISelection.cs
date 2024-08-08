using System;
using UnityEngine;

namespace MicroTowerDefence
{
    public interface ISelection
    {
        event Action<bool> OnInteractionEvent;
        event Action<bool> OnBuildingEvent;
        Vector3 GridPosition { get; }

        void OnSellTower();
        void OnUpgradeTower();
    }
}