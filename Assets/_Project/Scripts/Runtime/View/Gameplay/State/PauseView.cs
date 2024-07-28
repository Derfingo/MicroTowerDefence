using DG.Tweening;
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

    public override void Show()
    {
        GetComponent<RectTransform>().DOLocalMove(Vector3.zero, 0.5f, false).SetEase(Ease.InOutQuart);
    }

    public override void Hide()
    {
        GetComponent<RectTransform>().DOLocalMove(new Vector3(0f, Screen.height * 2, 0f), 0.5f, false).SetEase(Ease.InOutQuart)
            .SetLink(gameObject);
    }
}
