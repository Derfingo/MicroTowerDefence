using System;
using UnityEngine;

namespace MicroTowerDefence
{
    public interface IPath
    {
        void Initialize(Transform[] path, MovementType type, float maxDistance, float speed);
    }

    [Serializable]
    public class PathConfig
    {
        public MovementType MovementType;
        public Vector3 Direction => (Points[1].position - Points[0].position).normalized;
        public Vector3 InitialPoint => Points[0].position;
        public float LeastDistance = 0.1f;
        public Transform[] Points;
    }
}