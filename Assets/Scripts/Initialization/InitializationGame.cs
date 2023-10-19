using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class InitializationGame : MonoBehaviour
{
    [SerializeField] private GameTileContentFactory _contentFactory;
    [SerializeField] private EnemyFactory _enemyFactory;
    [SerializeField] private TileBuilder _tileBuilder;
    [SerializeField] private GameBoard _board;
    [SerializeField] private Camera _camera;
    [Space]
    [SerializeField, Range(0.1f, 10f)] private float _spawnSpeed;

    private readonly EnemyCollection _enemies = new();
    private float _spawnProgress;
    private bool _isSpawning;

    private void Start()
    {
        BoardData boardData = BoardData.GetInitial(new Vector2Int(10, 10));
        _board.Initialize(boardData, _contentFactory);
        _tileBuilder.Initialize(_contentFactory, _camera, _board);
        _isSpawning = false;

        BeginNewGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _isSpawning = !_isSpawning;
        }

        if (_isSpawning)
        {
            _spawnProgress += _spawnSpeed * Time.deltaTime;
            while (_spawnProgress >= 1f)
            {
                _spawnProgress -= 1f;
                SpawnEnemy();
            }

            _enemies.GameUpdate();
        }
    }

    private void BeginNewGame()
    {
        Cleanup();
        _tileBuilder.Enable();
    }

    private void Cleanup()
    {
        _tileBuilder.Disable();
        _board.Clear();
    }

    private void SpawnEnemy()
    {
        GameTile spawnPoint = _board.GetSpawnPoint(UnityEngine.Random.Range(0, _board.SpawnPointCount));
        Enemy enemy = _enemyFactory.Get();
        enemy.SpawnOn(spawnPoint);
        _enemies.Add(enemy);
    }
}
