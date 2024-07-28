using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace MicroTowerDefence
{
    public class WinView : ViewBase
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _nextLevelButton;

        public Button NextLevelButton => _nextLevelButton;
        public Button RestartButton => _restartButton;

        public override void Show()
        {
            GetComponent<RectTransform>().DOScale(1f, 0.3f).SetEase(Ease.InOutBack);
        }

        public override void Hide()
        {
            GetComponent<RectTransform>().DOScale(0f, 0.3f).SetEase(Ease.InOutBack);
        }
    }
}