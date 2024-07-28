using DG.Tweening;
using UnityEngine.UI;
using UnityEngine;

namespace MicroTowerDefence
{
    public class DefeatView : ViewBase
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _mainViewButton;

        public Button MainViewButton => _mainViewButton;
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