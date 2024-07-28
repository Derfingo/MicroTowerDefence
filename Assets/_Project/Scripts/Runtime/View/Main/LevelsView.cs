using DG.Tweening;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace MicroTowerDefence
{
    public class LevelsView : ViewBase
    {
        [SerializeField] private RectTransform _buttonsPanel;
        [SerializeField] private LevelButton[] _levelButtons;

        public LevelButton[] LevelButtons => _levelButtons;

        public override void Show()
        {
            base.Show();
            AppeareButtons();
        }

        private async void AppeareButtons()
        {
            _buttonsPanel.DOLocalMoveY(0f, 0.5f).SetEase(Ease.InOutQuart);

            await Task.Delay(200);

            foreach (var button in _levelButtons)
            {
                button.Image.DOFade(1f, 0.5f).SetLink(gameObject);
            }
        }
    }
}