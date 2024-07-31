using DG.Tweening;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MicroTowerDefence
{
    public class LevelsView : ViewBase
    {
        [SerializeField] private RectTransform _buttonsPanel;
        [SerializeField] private GridLayoutGroup _buttonsGroup;

        public List<LevelButton> LevelButtons { get; private set; } = new();

        [Inject]
        public void Initialize()
        {
            for (int i = 0; i < _buttonsGroup.transform.childCount; i++)
            {
                var button = _buttonsGroup.transform.GetChild(i).GetComponent<LevelButton>();
                button.Initialize(i + 1);
                LevelButtons.Add(button);
            }
        }

        public override void Show()
        {
            base.Show();
            AppeareButtons();
        }

        private async void AppeareButtons()
        {
            _buttonsPanel.DOLocalMoveY(0f, 0.5f).SetEase(Ease.InOutQuart);

            await Task.Delay(200);

            foreach (var button in LevelButtons)
            {
                button.image.DOFade(1f, 0.5f).SetLink(gameObject);
            }
        }
    }
}