using UnityEngine;
using UnityEngine.UI;

public class TowerMenu : ViewBase
{
    [SerializeField] private Button _upgradeTowerButton;
    [SerializeField] private Button _sellTowerButton;

    public Button UpgradeButton => _upgradeTowerButton;
    public Button SellButton => _sellTowerButton;
}
