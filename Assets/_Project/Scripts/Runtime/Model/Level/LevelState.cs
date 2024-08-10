using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace MicroTowerDefence
{
    public class LevelState : ILevelState, IPrepare, IUpdate
    {
        public event Action OnWinEvent;
        public event Action OnDefeatEvent;
        public event Action OnPrepareEvent;
        public event Action<bool> OnPauseEvent;

        private readonly IInputActions _input;
        private readonly IPause[] _pauses;
        private readonly IReset[] _resets;
        private readonly ILoader _loader;
        private readonly IStart _start;
        private readonly Health _health;
        private readonly Scenario _scenario;
        private readonly EnemyContorller _enemyController;

        private readonly int _prepareTime;
        private bool _isPause;

        public LevelState(int prepareTime,
                         IStart start,
                         IPause[] pauses,
                         IReset[] resets,
                         IInputActions input,
                         ILoader loader,
                         EnemyContorller enemyController,
                         Health health,
                         Scenario scenario)
        {
            _input = input;
            _start = start;
            _pauses = pauses;
            _resets = resets;
            _health = health;
            _loader = loader;
            _scenario = scenario;
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
            ResetValues();
            _start.Reset();
            OnPrepareEvent?.Invoke();
        }

        private async void OnBeginLevel()
        {
            await Task.Delay(_prepareTime * 1000);
            _input.Enable();
            OnPause(false);
            _scenario.Begin();
        }

        public void GameUpdate()
        {
            if (_isPause) return;

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

            foreach (var item in _pauses)
            {
                item.Pause(_isPause);
            }

            if (isNotify)
            {
                OnPauseEvent?.Invoke(_isPause);
            }
        }

        private void ResetValues()
        {
            foreach (var reset in _resets)
            {
                reset.Reset();
            }
        }

        public void OnRestart()
        {
            OnPause(true);
            PrepareToStart();
        }

        ~LevelState()
        {
            _input.GamePauseEvent -= () => OnPause(false);
            _start.OnStartEvent -= OnBeginLevel;
            _health.OnHealthOverEvent -= OnDefeat;
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}