using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace MicroTowerDefence
{
    public class LevelState : MonoBehaviour, ILevelState, IPrepare
    {
        public event Action OnWinEvent;
        public event Action OnDefeatEvent;
        public event Action OnPrepareEvent;
        public event Action<bool> OnPauseEvent;

        private ILoader _loader;
        private IInputActions _input;
        private IStart _start;
        private Health _health;
        private Scenario _scenario;
        private LevelCycle _levelCycle;
        private EnemyContorller _enemyController;

        private int _prepareTime;
        private bool _isPause;

        [Inject]
        public void Initialize(int prepareTime,
                         IStart start,
                         IInputActions input,
                         ILoader loader,
                         EnemyContorller enemyController,
                         LevelCycle levelCycle,
                         Health health,
                         Scenario scenario)
        {
            _input = input;
            _start = start;
            _health = health;
            _loader = loader;
            _scenario = scenario;
            _levelCycle = levelCycle;
            _prepareTime = prepareTime;
            _enemyController = enemyController;

            _input.GamePauseEvent += () => OnPause(true);
            _start.OnStartEvent += OnBeginLevel;
            _health.OnHealthOverEvent += OnDefeat;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            PrepareToStart();
        }

        private void PrepareToStart()
        {
            OnPause(false);
            _input.Disable();
            _levelCycle.ResetValues();
            _start.Reset();
            OnPrepareEvent?.Invoke();
        }

        private void Update()
        {
            if (_isPause) return;

            UpdateScenario();
        }

        private async void OnBeginLevel()
        {
            await Task.Delay(_prepareTime * 1000);
            OnPause(false);
            _input.Enable();
            _scenario.Begin();
        }

        private void UpdateScenario()
        {
            _scenario.GameUpdate();

            if (_scenario.IsEnd && _enemyController.IsEmpty)
            {
                OnWinEvent?.Invoke();
                OnPause(false);
            }
        }

        public void OnMainMenu()
        {
            _loader.LoadAsync(Constants.Scenes.MAIN_MENU);
        }

        private void OnDefeat()
        {
            OnDefeatEvent?.Invoke();
            OnPause(false);
        }

        public void OnPause(bool isNotify) // fix signature
        {
            _isPause = !_isPause;
            _levelCycle.Pause(_isPause);
            _enemyController.Pause(_isPause);

            if (isNotify)
            {
                OnPauseEvent?.Invoke(_isPause);
            }
        }

        public void OnRestart()
        {
            OnPause(true);
            PrepareToStart();
        }

        private void OnDestroy()
        {
            _input.GamePauseEvent -= () => OnPause(false);
            _start.OnStartEvent -= OnBeginLevel;
            _health.OnHealthOverEvent -= OnDefeat;
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}