using UnityEngine;
using UnityEngine.UI;

public class MainView : ViewBase
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _settingButton;
    [SerializeField] private Button _quitButton;

    public Button StartButton => _startButton;
    public Button SettingButton => _settingButton;
    public Button QuitButton => _quitButton;
}
