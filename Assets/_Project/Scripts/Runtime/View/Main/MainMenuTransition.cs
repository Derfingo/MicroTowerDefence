using UnityEditor;
using UnityEngine;
using Zenject;

namespace MicroTowerDefence
{
    public class MainMenuTransition : ViewBase
    {
        [SerializeField] private MainView _mainView;
        [SerializeField] private SettingsView _settingsView;
        [SerializeField] private LevelsView _levelsView;

        private ViewBase _currentView;

        [Inject]
        public void Initialize()
        {
            SetInitialView();
            AddListeners();
        }

        private void SetInitialView()
        {
            _mainView.Show();
            _currentView = _mainView;
            _levelsView.Hide();
            _settingsView.Hide();
        }

        private void AddListeners()
        {
            _mainView.StartButton.OnClickEvent += ShowLevels;
            _mainView.SettingButton.OnClickEvent += ShowSetting;
            _mainView.QuitButton.OnClickEvent += QuitGame;
        }

        private void ShowMain()
        {
            _currentView.Hide();
            _mainView.Show();
            _currentView = _mainView;
        }

        private void ShowSetting()
        {
            _currentView.Hide();
            _settingsView.Show();
            _currentView = _settingsView;
        }

        private void ShowLevels()
        {
            _currentView.Hide();
            _levelsView.Show();
            _currentView = _levelsView;
        }

        private void QuitGame()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }

    public enum MainViewState
    {
        Main,
        Settings,
        Levels
    }
}