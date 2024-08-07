using System;
using UnityEngine;

namespace MicroTowerDefence
{
    public interface ISelection
    {
        event Action<bool> OnBuildingEvent;
        Vector3 GridPosition { get; }
    }
}