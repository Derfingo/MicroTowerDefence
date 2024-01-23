using UnityEditor;
using UnityEngine;

public class MainViewController : MonoBehaviour
{
    [SerializeField] private MainView _mainView;
    [SerializeField] private SettingView _settingView;
    [SerializeField] private LevelsView _levelsView;

    ViewBase _currentView;

    private void Start()
    {
        SetInitialView();
        AddListeners();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowMain();
        }
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
        _mainView.QuitButton.onClick.AddListener(() => EditorApplication.isPlaying = false);
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
        _settingView.Show();
        _currentView = _settingView;
    }

    private void ShowLevels()
    {
        _currentView.Hide();
        _levelsView.Show();
        _currentView = _levelsView;
    }
}

public enum MainViewState
{
    Main,
    Settings,
    Levels
}