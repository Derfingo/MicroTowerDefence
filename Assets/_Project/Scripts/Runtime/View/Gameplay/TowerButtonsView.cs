using TMPro;
using UnityEngine;

namespace MicroTowerDefence
{
    public class TowerButtonsView : ViewBase
    {
        [SerializeField] private ButtonView _upgradeTowerButton;
        [SerializeField] private ButtonView _sellTowerButton;
        [SerializeField] private TMP_Text _upgradeCostText;
        [SerializeField] private TMP_Text _sellCostText;

        public ButtonView UpgradeButton => _upgradeTowerButton;
        public ButtonView SellButton => _sellTowerButton;

        public void SetCosts(uint upgradeCost, uint sellCost)
        {
            _upgradeCostText.text = upgradeCost.ToString();
            _sellCostText.text = sellCost.ToString();
        }
    }
}