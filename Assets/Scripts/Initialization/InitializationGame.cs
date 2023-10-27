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
    [SerializeField] private WarFactory _warFactory;
    [SerializeField] private GameBoard _board;
    [SerializeField] private Camera _camera;
    [Space]
    [SerializeField, Range(0.1f, 10f)] private float _spawnSpeed;

    private static InitializationGame _instance;

    private readonly GameBehaviourCollection _enemies = new();
    private readonly GameBehaviourCollection _nonEnemies = new();
    private float _spawnProgress;
    private bool _isSpawning;

    private void Start()
    {
        BoardData boardData = BoardData.GetInitial(new Vector2Int(11, 11));
        _board.Initialize(boardData, _contentFactory);
        _tileBuilder.Initialize(_contentFactory, _camera, _board);
        _isSpawning = false;

        BeginNewGame();
    }

    public static Shell SpawnShell()
    {
        Shell shell = _instance._warFactory.Shell;
        _instance._nonEnemies.Add(shell);
        return shell;
    }

    public static Explosion SpawnExplosion()
    {
        Explosion explosion = _instance._warFactory.Explosion;
        _instance._nonEnemies.Add(explosion);
        return explosion;
    }

    private void OnEnable()
    {
        _instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
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
            Physics.SyncTransforms();
        }

        _board.GameUpdate();
        _nonEnemies.GameUpdate();
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
