using System;
using UnityEngine;

public class GameplayViewController : ViewBase
{
    [SerializeField] private TowerMenu _towerMenu;
    [SerializeField] private BuildingView _buildingView;
    [SerializeField] private TowerFactory _towerFactory;
    [SerializeField] private CoinsView _coinsView;

    public event Action<TowerType> OnSelectBuilding;

    public event Action OnUpgradeBuilding;
    public event Action OnSellBuilding;

    private void Start()
    {
        _buildingView.Buttons.ForEach(b => b.AddListener(OnSelectedBuilding));
        _towerMenu.UpgradeButton.onClick.AddListener(OnUpgradeTowerClicked);
        _towerMenu.SellButton.onClick.AddListener(OnSellTowerCliked);

        SetConfig();
    }

    public void ShowDeficiencyCoins()
    {
        _coinsView.DeficiencyCoinsAnimate();
    }

    public void ShowBuildingMenu()
    {
        HideMenus();
        _buildingView.Show();
    }

    public void ShowTowerMenu(uint upgradeCost, uint sellCost)
    {
        HideMenus();
        _towerMenu.SetCosts(upgradeCost, sellCost);
        _towerMenu.Show();
    }

    private void HideMenus()
    {
        _buildingView.Hide();
        _towerMenu.Hide();
    }

    private void SetConfig()
    {
        foreach (var button in _buildingView.Buttons)
        {
            button.SetCost(_towerFactory.GetConfig(button.Type).CostToBuild);
        }
    }

    private void OnSelectedBuilding(TowerType type)
    {
        OnSelectBuilding?.Invoke(type);
    }

    private void OnUpgradeTowerClicked()
    {
        OnUpgradeBuilding?.Invoke();
    }

    private void OnSellTowerCliked()
    {
        OnSellBuilding?.Invoke();
    }
}
