using TMPro;
using UnityEngine;

public class TowerButtonsView : ViewBase
{
    [SerializeField] private SimpleButton _upgradeTowerButton;
    [SerializeField] private SimpleButton _sellTowerButton;
    [SerializeField] private TMP_Text _upgradeCostText;
    [SerializeField] private TMP_Text _sellCostText;

    public SimpleButton UpgradeButton => _upgradeTowerButton;
    public SimpleButton SellButton => _sellTowerButton;

    public void SetCosts(uint upgradeCost, uint sellCost)
    {
        _upgradeCostText.text = upgradeCost.ToString();
        _sellCostText.text = sellCost.ToString();
    }
}
