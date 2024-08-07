using System;
using UnityEngine;

namespace MicroTowerDefence
{
    public interface IGrid
    {
        event Action<Vector3, bool> OnGridEvent;
    }
}