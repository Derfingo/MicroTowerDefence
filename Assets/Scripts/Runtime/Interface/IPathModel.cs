using UnityEngine;

public interface IPathModel
{
    void Initialize(Transform[] path, MovementType type, float maxDistance, float speed);
}
