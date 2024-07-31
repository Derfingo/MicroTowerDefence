using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

namespace MicroTowerDefence
{
    public class MainView : ViewBase
    {
        [SerializeField] private TMP_Text _label;
        [SerializeField] private RectTransform _buttonsPanel;
        [SerializeField] private ButtonView _startButton;
        [SerializeField] private ButtonView _settingButton;
        [SerializeField] private ButtonView _quitButton;

        public ButtonView StartButton => _startButton;
        public ButtonView SettingButton => _settingButton;
        public ButtonView QuitButton => _quitButton;

        [Inject]
        public void Initialize()
        {
            InitializeButtons();
            MoveLabel();
            AppeareButtons();
        }

        private void InitializeButtons()
        {
            _startButton.Initialize();
            _settingButton.Initialize();
            _quitButton.Initialize();
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