using System.Collections;
using UnityEngine;

public class GameCycle : MonoBehaviour
{
    private ContentSelectionView _contentSelectionView;
    [SerializeField] private ProjectileController _projectileController;
    [SerializeField] private TilemapController _tilemapController;
    [SerializeField] private TowerController _buildingController;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private EnemyContorller _enemyController;
    [SerializeField] private GameScenario _gameScenario;
    [SerializeField] private ActionMapReader _input;
    [SerializeField] private Health _health;
    [SerializeField] private Coins _coins;

    private GameScenario.State _activeScenario;
    private Coroutine _prepareRoutine;
    private bool _scenarioInProgress;
    private float _prepareTime;
    private bool _isDefeat;
    private bool _isPause;

    public void Initialize(float prepareTime, ContentSelectionView contentSelectionView)
    {
        _contentSelectionView = contentSelectionView;
        _prepareTime = prepareTime;
        _input.GamePauseEvent += OnPause;
        _enemyController.OnEnemyFinish += TakeDamage;
        _enemyController.OnEnemyDie += AddCoins;
        BeginNewGame();
    }

    private void Update()
    {
        if (_isPause)
        {
            return;
        }

        UpdateControllers();
        UpdateScenario();
        Physics.SyncTransforms();
    }

    private void LateUpdate()
    {
        if (_isPause)
        {
            return;
        }

        _cameraController.GameLateUpdate();
    }

    private void OnPause()
    {
        _isPause = !_isPause;
        _enemyController.SetPause(_isPause);
    }

    private void UpdateScenario()
    {
        if(_scenarioInProgress)
        {
            if (_isDefeat)
            {
                Debug.Log("Defeated");
                BeginNewGame();
            }

            if (_activeScenario.Progress() == false && _enemyController.IsEmpty)
            {
                Debug.Log("Win");
                BeginNewGame();
                _activeScenario.Progress();
            }
        }
    }

    private void TakeDamage(uint damage)
    {
        _isDefeat = _health.TryTakeDamage(damage);
    }

    private void AddCoins(uint coins)
    {
        _coins.Add(coins);
    }

    private void UpdateControllers()
    {
        _tilemapController.GameUpdate();
        _contentSelectionView.GameUpdate();
        _buildingController.GameUpdate();
        _projectileController.GameUpdate();
        _enemyController.GameUpdate();
    }

    private void BeginNewGame()
    {
        _scenarioInProgress = false;
        if (_prepareRoutine != null)
        {
            StopCoroutine(_prepareRoutine);
        }
        Cleanup();
        _activeScenario = _gameScenario.Begin(_enemyController);
        _prepareRoutine = StartCoroutine(PrepareRoutine());
    }

    private IEnumerator PrepareRoutine()
    {
        yield return new WaitForSeconds(_prepareTime);
        _activeScenario = _gameScenario.Begin(_enemyController);
        _scenarioInProgress = true;
    }

    private void Cleanup()
    {
        _projectileController.Clear();
        _buildingController.Clear();
        _enemyController.Clear();
        _health.Reset();
        _coins.Reset();
        _isDefeat = false;
    }
}