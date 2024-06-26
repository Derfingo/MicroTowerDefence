using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CollisionDetection : ScriptableObject
{
    [SerializeField] private LayerMask[] _layers;

    public LayerMask[] Layers => _layers;
}
