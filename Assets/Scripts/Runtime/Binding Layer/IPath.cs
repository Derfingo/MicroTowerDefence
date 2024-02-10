using UnityEngine;

namespace MicroTowerDefence
{
    public interface IPath
    {
        void Initialize(Transform[] path, MovementType type, float maxDistance, float speed);
    }
}