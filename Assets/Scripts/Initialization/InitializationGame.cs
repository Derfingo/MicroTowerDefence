using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializationGame : MonoBehaviour
{
    [SerializeField] private GameBoard _board;
    private Vector2Int _boardSize;

    void Start()
    {
        _boardSize = new Vector2Int (10, 10);
        _board.Initialize(_boardSize);
    }
}
