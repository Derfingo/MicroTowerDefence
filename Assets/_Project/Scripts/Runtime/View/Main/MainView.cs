using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MicroTowerDefence
{
    public class MainView : ViewBase
    {
        [SerializeField] private TMP_Text _label;
        [SerializeField] private RectTransform _buttonsPanel;
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _settingButton;
        [SerializeField] private Button _quitButton;

        public Button StartButton => _startButton;
        public Button SettingButton => _settingButton;
        public Button QuitButton => _quitButton;

        private void Start()
        {
            MoveLabel();
            AppeareButtons();
        }

        private void MoveLabel()
        {
            _label.rectTransform.DOLocalMoveY(550f, 0.5f)
                .SetEase(Ease.InOutQuart)
                .SetLink(gameObject)
                .OnKill(AppeareButtons);
        }

        private void AppeareButtons()
        {
            DOTween.Sequence()
                .Append(_buttonsPanel.DOLocalMoveY(100f, 0.2f))
                .SetEase(Ease.InOutQuart)
                .Append(_startButton.image.DOFade(1f, 0.2f))
                .Append(_settingButton.image.DOFade(1f, 0.2f))
                .Append(_quitButton.image.DOFade(1f, 0.2f));
        }
    }
}