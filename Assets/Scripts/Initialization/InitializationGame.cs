using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using Random = UnityEngine.Random;

public class InitializationGame : MonoBehaviour
{
    [SerializeField] private Vector2Int _cellCount = new(11, 11);
    [SerializeField, Range(0f, 30f)] private float _prepareTime = 5f;
    [SerializeField, Range(10, 100)] private int _startingPlayerHealth = 10;
    [Space]
    [SerializeField] private GameTileContentFactory _contentFactory;
    [SerializeField] private EnemyFactory _enemyFactory;
    [SerializeField] private TileBuilder _tileBuilder;
    [SerializeField] private WarFactory _warFactory;
    [SerializeField] private GameBoard _board;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameSceraio _sceraio;

    private GameSceraio.State _activeScenario;
    private static InitializationGame _instance;

    private readonly GameBehaviourCollection _enemies = new();
    private readonly GameBehaviourCollection _nonEnemies = new();

    private int _currentPlayerHealth;
    private bool _scenarioInProgress;
    private bool _isPaused;

    private void Start()
    {
        BoardData boardData = BoardData.GetInitial(_cellCount);
        _board.Initialize(boardData, _contentFactory);
        _tileBuilder.Initialize(_contentFactory, _camera, _board);
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
        if (Input.GetKeyDown(KeyCode.P))
        {
            _isPaused = !_isPaused;
            Time.timeScale = _isPaused ? 0f : 1f;
        }

        if (_scenarioInProgress)
        {
            if (_currentPlayerHealth <= 0f)
            {
                Debug.Log("Defeated");
                BeginNewGame();
            }

            if (_activeScenario.Progress() == false && _enemies.IsEmpty)
            {
                Debug.Log("Win");
                BeginNewGame();
                _activeScenario.Progress();
            }
        }

        Physics.SyncTransforms();
        _enemies.GameUpdate();
        _board.GameUpdate();
        _nonEnemies.GameUpdate();
    }

    private void BeginNewGame()
    {
        _scenarioInProgress = false;
        if (_prepareRoutine != null)
        {
            StopCoroutine( _prepareRoutine );
        }

        _currentPlayerHealth = _startingPlayerHealth;
        Cleanup();
        _activeScenario = _sceraio.Begin();
        _tileBuilder.Enable();
        _prepareRoutine = StartCoroutine(PrepareRoutine());
    }

    private void Cleanup()
    {
        _tileBuilder.Disable();
        _board.Clear();
        _enemies.Clear();
        _nonEnemies.Clear();
    }

    public static void SpawnEnemy(EnemyFactory factory, EnemyType type)
    {
        GameTile spawnPoint = _instance._board.GetSpawnPoint(Random.Range(0, _instance._board.SpawnPointCount));
        Enemy enemy = factory.Get(type);
        enemy.SpawnOn(spawnPoint);
        _instance._enemies.Add(enemy);
    }

    public static void EnemyReachedDestination()
    {
        _instance._currentPlayerHealth--;
    }

    private Coroutine _prepareRoutine;

    private IEnumerator PrepareRoutine()
    {
        yield return new WaitForSeconds(_prepareTime);
        _activeScenario = _sceraio.Begin();
        _scenarioInProgress = true;
    }
}
