using MicroTowerDefence;
using UnityEngine;
using UnityEngine.UI;

public class PauseView : ViewBase
{
    [SerializeField] private Button _countinueButton;
    [SerializeField] private Button _mainViewButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _restartButton;

    public Button CountinueButton => _countinueButton;
    public Button MainViewButton => _mainViewButton;
    public Button SettingsButton => _settingsButton;
    public Button RestartButton => _restartButton;
}
