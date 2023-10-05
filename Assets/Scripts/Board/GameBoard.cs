using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    [SerializeField] private Transform _ground;
    private Vector2Int _size;

    public void Initialize(Vector2Int size)
    {
        _size = size;
        _ground.localScale = new Vector3(size.x, size.y, 1f);
    }
}
