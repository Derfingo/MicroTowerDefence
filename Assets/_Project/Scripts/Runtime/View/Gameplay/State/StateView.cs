using System;
using UnityEngine;
using Zenject;

namespace MicroTowerDefence
{
    public class StateView : ViewBase, IStateView
    {
        [SerializeField] private SettingsView _settingsView;
        [SerializeField] private DefeatView _defeatView;
        [SerializeField] private PauseView _pauseView;
        [SerializeField] private WinView _winView;

        public event Action OnNextLevelEvent;
        public event Action OnContinueEvent;
        public event Action OnMianMenuEvent;
        public event Action OnSettingsEvent;
        public event Action OnRestartEvent;

        [Inject]
        public void Initialize()
        {
            HideMenus();

            _pauseView.CountinueButton.onClick.AddListener(OnContinue);
            _pauseView.RestartButton.onClick.AddListener(OnRestart);
            _pauseView.SettingsButton.onClick.AddListener(OnSettings);
            _pauseView.MainViewButton.onClick.AddListener(OnMainMenu);

            _defeatView.RestartButton.onClick.AddListener(OnRestart);
            _defeatView.MainViewButton.onClick.AddListener(OnMainMenu);

            _winView.NextLevelButton.onClick.AddListener(OnNextLevel);
            _winView.RestartButton.onClick.AddListener(OnRestart);
        }

        public void ShowDefeatMenu()
        {
            _defeatView.Show();
        }

        public void ShowPauseMenu(bool isEnable)
        {
            if (isEnable)
            {
                _pauseView.Show();
            }
            else
            {
                _pauseView.Hide();
            }
        }

        public void ShowWinMenu()
        {
            _winView.Show();
        }

        public void HideMenus()
        {
            _defeatView.Hide();
            _pauseView.Hide();
            _winView.Hide();
        }

        private void OnContinue()
        {
            OnContinueEvent?.Invoke();
        }

        private void OnRestart()
        {
            HideMenus();
            OnRestartEvent?.Invoke();
        }

        private void OnSettings()
        {
            HideMenus();
            _settingsView.Show();
            OnSettingsEvent?.Invoke();
        }

        private void OnMainMenu()
        {
            OnMianMenuEvent?.Invoke();
        }

        private void OnNextLevel()
        {
            OnNextLevelEvent?.Invoke();
        }
    }
}