using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerMenu : ViewBase
{
    [SerializeField] private Button _upgradeTowerButton;
    [SerializeField] private Button _sellTowerButton;
    [SerializeField] private TextMeshProUGUI _upgradeCostText;
    [SerializeField] private TextMeshProUGUI _sellCostText;

    public Button UpgradeButton => _upgradeTowerButton;
    public Button SellButton => _sellTowerButton;

    public void SetCosts(uint upgradeCost, uint sellCost)
    {
        _upgradeCostText.text = upgradeCost.ToString();
        _sellCostText.text = sellCost.ToString();
    }
}
