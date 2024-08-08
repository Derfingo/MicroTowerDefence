using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace MicroTowerDefence
{
    public class TowerInterationView : ViewBase, ITowerInteractionView
    {
        public event Action OnSellTowerEvent;
        public event Action OnUpgradeTowerEvent;

        public event Action OnEnterButtonEvent;
        public event Action OnExitButtonEvent;

        [SerializeField] private ButtonView _upgradeButton;
        [SerializeField] private ButtonView _sellButton;

        [SerializeField] private TextMeshProUGUI _upgradeText;
        [SerializeField] private TextMeshProUGUI _sellText;

        [Inject]
        public void Initialize()
        {
            _upgradeButton.OnClickEvent += OnUpgradeTower;
            _sellButton.OnClickEvent += OnSellTower;

            _upgradeButton.OnPointerEnterEvent += OnEnterButton;
            _upgradeButton.OnPointerExitEvent += OnExitButton;

            _sellButton.OnPointerEnterEvent += OnEnterButton;
            _sellButton.OnPointerExitEvent += OnExitButton;
        }

        public void SetCosts(uint upgradeCost, uint sellCost)
        {
            _upgradeText.text = upgradeCost.ToString();
            _sellText.text = sellCost.ToString();
        }

        private void OnEnterButton()
        {
            OnEnterButtonEvent?.Invoke();
        }

        private void OnExitButton()
        {
            OnExitButtonEvent?.Invoke();
        }
        private void OnSellTower()
        {
            OnSellTowerEvent?.Invoke();
        }

        private void OnUpgradeTower()
        {
            OnUpgradeTowerEvent?.Invoke();
        }

        private void OnDestroy()
        {
            _upgradeButton.OnClickEvent -= OnUpgradeTowerEvent;
            _sellButton.OnClickEvent -= OnSellTowerEvent;

            _upgradeButton.OnPointerEnterEvent -= OnEnterButtonEvent;
            _upgradeButton.OnPointerExitEvent -= OnExitButtonEvent;

            _sellButton.OnPointerEnterEvent -= OnEnterButtonEvent;
            _sellButton.OnPointerExitEvent -= OnExitButtonEvent;
        }
    }
}
