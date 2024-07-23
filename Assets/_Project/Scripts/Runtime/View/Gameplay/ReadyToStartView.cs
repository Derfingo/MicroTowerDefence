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

        public event Action OnStartEvent;

        private IInputActions _input;

        [Inject]
        public void Initialize(IInputActions input)
        {
            _input = input;

            _input.OnStartEvent += OnStart;
            PulseText();
        }

        private void PulseText()
        {
            _readyToStartText.rectTransform.DOScale(Vector3.one * 1.1f, 0.4f).SetLoops(-1, LoopType.Yoyo)
                .SetLink(gameObject);
        }

        private void OnStart()
        {
            _readyToStartText.enabled = false;
            OnStartEvent?.Invoke();
        }
    }
}