using UnityEngine;

namespace MicroTowerDefence
{
    public interface IPathView
    {
        Transform[] Points { get; }
        Vector3 InitialPoint { get; }
        float LeastDistance { get; }
    }
}