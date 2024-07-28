using UnityEditor;
using UnityEngine;

namespace MicroTowerDefence
{
    public class MenuTransition : ViewBase
    {
        [SerializeField] private MainView _mainView;
        [SerializeField] private SettingsView _settingsView;
        [SerializeField] private LevelsView _levelsView;

        ViewBase _currentView;

        private void Start()
        {
            SetInitialView();
            AddListeners();
        }

        private void SetInitialView()
        {
            _mainView.Show();
            _currentView = _mainView;
        }

        private void AddListeners()
        {
            _mainView.StartButton.onClick.AddListener(ShowLevels);
            _mainView.SettingButton.onClick.AddListener(ShowSetting);
            _mainView.QuitButton.onClick.AddListener(QuitGame);
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