using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace MicroTowerDefence
{
    public class LevelState : MonoBehaviour, ILevelState
    {
        public event Action OnWinEvent;
        public event Action OnPauseEvent;
        public event Action OnDefeatEvent;

        private IInputActions _input;
        private IStart _start;
        private Health _health;
        private LevelCycle _levelCycle;
        private GameScenario _scenario;
        private EnemyContorller _enemyController;

        private float _prepareTime;
        private bool _scenarioInProgress;
        private Coroutine _prepareRoutine;
        private GameScenario.State _activeScenario;

        private bool _isDefeat;
        private bool _isPause;

        [Inject]
        public void Initialize(float prepareTime,
                         IStart start,
                         IInputActions input,
                         EnemyContorller enemyController,
                         GameScenario scenario,
                         LevelCycle levelCycle,
                         Health health)
        {
            _input = input;
            _start = start;
            _health = health;
            _scenario = scenario;
            _levelCycle = levelCycle;
            _prepareTime = prepareTime;
            _enemyController = enemyController;

            _input.GamePauseEvent += OnPause;
            _start.OnStartEvent += OnBeginLevel;
            _health.OnHealthOverEvent += OnDefeat;

            PrepareToStart();
        }

        private void PrepareToStart()
        {
            _levelCycle.ResetValues();
            _isDefeat = false;
        }

        private void Update()
        {
            if (_isPause)
            {
                return;
            }

            UpdateScenario();
        }

        private void OnBeginLevel()
        {
            _scenarioInProgress = false;
            if (_prepareRoutine != null)
            {
                StopCoroutine(_prepareRoutine);
            }

            _activeScenario = _scenario.Begin(_enemyController);
            _prepareRoutine = StartCoroutine(PrepareRoutine());
        }

        private IEnumerator PrepareRoutine()
        {
            yield return new WaitForSeconds(_prepareTime);
            _activeScenario = _scenario.Begin(_enemyController);
            _scenarioInProgress = true;
        }

        private void UpdateScenario()
        {
            if (_scenarioInProgress)
            {
                if (_isDefeat)
                {
                    Debug.Log("Defeated");
                    OnDefeatEvent?.Invoke();
                    OnPause();
                }

                if (_activeScenario.Progress() == false && _enemyController.IsEmpty)
                {
                    Debug.Log("Win");
                    OnWinEvent?.Invoke();
                    OnPause();
                    _activeScenario.Progress();
                }
            }
        }

        private void OnDefeat()
        {
            _isDefeat = true;
        }

        private void OnPause()
        {
            _isPause = !_isPause;
            _levelCycle.Pause(_isPause);
            _enemyController.Pause(_isPause);
            OnPauseEvent?.Invoke();
        }
    }
}