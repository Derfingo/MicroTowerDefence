using System.Collections;
using UnityEngine;

public class GameCycle : MonoBehaviour
{
    [SerializeField, Range(0f, 30f)] private float _prepareTime = 5f;
    [Space]
    [SerializeField] private ProjectileController _projectileController;
    [SerializeField] private BuildingController _buildingController;
    [SerializeField] private EnemyContorller _enemyController;
    [SerializeField] private GameScenario _gameScenario;
    [SerializeField] private Health _health;
    [SerializeField] private Coins _coins;

    private GameScenario.State _activeScenario;
    private Coroutine _prepareRoutine;
    private bool _scenarioInProgress;
    private bool _isDefeat;

    private void Start()
    {
        BeginNewGame();
        _enemyController.OnEnemyFinish += TakeDamage;
        _enemyController.OnEnemyDie += AddCoins;
    }

    private void Update()
    {
        UpdateScenario();
        Physics.SyncTransforms();
        UpdateEntities();
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

    private void UpdateEntities()
    {
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
