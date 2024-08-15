using System;
using UnityEditor;
using UnityEngine;

namespace MicroTowerDefence
{
    public class MainMenuTransition
    {
        public event Action<LevelsView> OnLevelsViewEvent;

        private  MainView _mainView;
        private  LevelsView _levelsView;
        private  SettingsView _settingsView;

        private readonly Transform _rootView;

        private readonly MainViewProvider _mainViewProvider;
        private readonly LevelsViewProvider _levelsViewProvider;
        private readonly SettingsViewProvider _settingsViewProvider;

        private ViewBase _currentView;

        public MainMenuTransition(MainViewProvider mainProvider,
                                  SettingsViewProvider settingsProvider,
                                  LevelsViewProvider levelsProvider,
                                  Transform root)
        {
            _rootView = root;
            _mainViewProvider = mainProvider;
            _levelsViewProvider = levelsProvider;
            _settingsViewProvider = settingsProvider;

            SetInitialView();
        }

        private async void SetInitialView()
        {
            _mainView = await _mainViewProvider.Load();
            _currentView = _mainView;
            _currentView.transform.SetParent(_rootView, false);
            _mainView.Initialize();
            _currentView.Show();

            _mainView.StartButton.OnClickEvent += ShowLevels;
            _mainView.SettingButton.OnClickEvent += ShowSetting;
            _mainView.QuitButton.OnClickEvent += QuitGame;
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

        private async void ShowSetting()
        {
            _mainView.StartButton.OnClickEvent -= ShowLevels;
            _mainView.SettingButton.OnClickEvent -= ShowSetting;
            _mainView.QuitButton.OnClickEvent -= QuitGame;

            _currentView.Hide();
            _mainViewProvider.Unload();
            _settingsView = await _settingsViewProvider.Load();
            _currentView = _settingsView;
            _currentView.transform.SetParent(_rootView, false);
            _settingsView.Initialize();
            _currentView.Show();
        }

        private async void ShowLevels()
        {
            _mainView.StartButton.OnClickEvent -= ShowLevels;
            _mainView.SettingButton.OnClickEvent -= ShowSetting;
            _mainView.QuitButton.OnClickEvent -= QuitGame;

            _currentView.Hide();
            _mainViewProvider.Unload();
            _levelsView = await _levelsViewProvider.Load();
            _currentView = _levelsView;
            _currentView.transform.SetParent(_rootView, false);
            _levelsView.Initialize();
            _currentView.Show();
            OnLevelsViewEvent?.Invoke(_levelsView);
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
}