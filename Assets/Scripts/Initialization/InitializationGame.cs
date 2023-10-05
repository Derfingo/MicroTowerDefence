using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializationGame : MonoBehaviour
{
    [SerializeField] private Vector2Int _boardSize;

    [SerializeField] private GameBoard _board;

    void Start()
    {
        _board.Initialize(_boardSize);
    }
}
