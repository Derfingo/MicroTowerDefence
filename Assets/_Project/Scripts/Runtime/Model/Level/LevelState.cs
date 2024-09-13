using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MicroTowerDefence
{
    public class LevelState : ILevelState, IPrepare, IUpdate, IDispose
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

            _input.OnPauseEvent += OnPause;
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
            SetPauseInternal(true);
            _input.Disable();
            ResetValues();
            _start.Reset();
            OnPrepareEvent?.Invoke();
        }

        private async void OnBeginLevel()
        {
            await Task.Delay(_prepareTime * 1000);
            _input.SetPlayerInput();
            SetPauseInternal(false);
            _scenario.Begin();
        }

        public void GameUpdate()
        {
            if (_isPause) return;

            if (_scenario.IsEnd && _enemyController.IsEmpty)
            {
                OnWinEvent?.Invoke();
                SetPauseInternal(true);
            }
        }

        public void OnMainMenu()
        {
            _loader.LoadAddressableAsync(Constants.Scenes.MAIN_MENU);
        }

        public void OnNextLevel()
        {
            _loader.LoadNextLevel();
        }

        private void OnDefeat()
        {
            OnDefeatEvent?.Invoke();
            SetPauseInternal(true);
        }

        public void OnPause()
        {
            _isPause = !_isPause;

            SetPauseInternal(_isPause);

            if (_isPause)
            {
                _input.SetUIInput();
            }
            else
            {
                _input.SetPlayerInput();
            }

            OnPauseEvent?.Invoke(_isPause);
        }

        private void SetPauseInternal(bool isEnable)
        {
            _isPause = isEnable;

            foreach (var item in _pauses)
            {
                item.Pause(_isPause);
            }

            Debug.Log(_isPause);
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
            OnPause();
            PrepareToStart();
        }

        public void Dispose()
        {
            _input.OnPauseEvent -= OnPause;
            _start.OnStartEvent -= OnBeginLevel;
            _health.OnHealthOverEvent -= OnDefeat;
            SceneManager.sceneLoaded -= OnSceneLoaded;
            //UnityEngine.Debug.Log($"Dispose: {GetType().Name}");
        }
    }
}