using UnityEngine;

public interface IPathView
{
    Transform[] Points { get; }
    Vector3 InitialPoint {  get; }
    float LeastDistance { get; }
}
