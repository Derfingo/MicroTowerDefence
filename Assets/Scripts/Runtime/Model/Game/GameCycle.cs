using System.Collections;
using UnityEngine;

namespace MicroTowerDefence
{
    public class GameCycle : MonoBehaviour
    {
        [SerializeField] private EnemyContorller _enemyController;
        [SerializeField] private GameScenario _gameScenario;
        [SerializeField] private Health _health;
        [SerializeField] private Coins _coins;

        private IInputActions _input;

        private GameScenario.State _activeScenario;
        private Coroutine _prepareRoutine;
        private bool _scenarioInProgress;
        private float _prepareTime;
        private bool _isDefeat;
        private bool _isPause;

        private IReset[] _resets;
        private IUpdate[] _updates;
        private ILateUpdate[] _lateUpdates;

        private PathConfig _pathConfig;

        public void Initialize(float prepareTime, IInputActions input, IReset[] resets, IUpdate[] updates, ILateUpdate[] lateUpdates, PathConfig config)  // fix
        {
            _input = input;
            _resets = resets;
            _updates = updates;
            _lateUpdates = lateUpdates;

            _pathConfig = config;

            _prepareTime = prepareTime;
            _input.GamePauseEvent += OnPause;
            _enemyController.EnemyFinishEvent += TakeDamage;
            _enemyController.EnemyDieEvent += AddCoins;
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

            LateUpdateControllers();
        }

        private void OnPause()
        {
            _isPause = !_isPause;
            _enemyController.SetPause(_isPause);
        }

        private void UpdateScenario()
        {
            if (_scenarioInProgress)
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
            foreach (var update in _updates)
            {
                update.GameUpdate();
            }
        }

        private void LateUpdateControllers()
        {
            foreach (var lateUpdate in _lateUpdates)
            {
                lateUpdate.GameLateUpdate();
            }
        }

        private void BeginNewGame()
        {
            _scenarioInProgress = false;
            if (_prepareRoutine != null)
            {
                StopCoroutine(_prepareRoutine);
            }
            Cleanup();
            _activeScenario = _gameScenario.Begin(_enemyController, _pathConfig);
            _prepareRoutine = StartCoroutine(PrepareRoutine());
        }

        private IEnumerator PrepareRoutine()
        {
            yield return new WaitForSeconds(_prepareTime);
            _activeScenario = _gameScenario.Begin(_enemyController, _pathConfig);
            _scenarioInProgress = true;
        }

        private void Cleanup()
        {
            foreach (var reset in _resets)
            {
                reset.Reset();
            }

            _isDefeat = false;
        }
    }
}