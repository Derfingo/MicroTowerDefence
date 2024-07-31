using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace MicroTowerDefence
{
    public class ReadyToStartView : ViewBase, IStart
    {
        [SerializeField] private TMP_Text _readyToStartText;
        [SerializeField] private ButtonView _startButton;

        public event Action OnStartEvent;

        private Tween _tween;

        [Inject]
        public void Initialize()
        {
            _startButton.OnClickEvent += OnStart;
            PulseText();
        }

        void IStart.Reset()
        {
            _startButton.Show();
        }

        private void PulseText()
        {
            _tween = _readyToStartText.rectTransform.DOScale(Vector3.one * 1.1f, 0.4f).SetLoops(-1, LoopType.Yoyo)
                .SetLink(gameObject);
        }

        private void OnStart()
        {
            _tween.Kill();
            _readyToStartText.rectTransform.DOScale(0f, 0.3f);
            _startButton.Hide();
            OnStartEvent?.Invoke();
        }
    }
}