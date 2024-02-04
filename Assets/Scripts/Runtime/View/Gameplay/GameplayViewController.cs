using System;
using UnityEngine;

public class GameplayViewController : ViewBase
{
    [SerializeField] private BuildTowerView _towerMenu;
    [SerializeField] private BuildingView _buildingView;
    [SerializeField] private TowerFactory _towerFactory;
    [SerializeField] private CoinsView _coinsView;

    public event Action<TowerType> BuildTowerEvent;
    public event Action<TowerType> SelectPreviewTowerEvent;
    public event Action<TowerType> DeselectPreviewTowerEvent;

    public event Action UpgradeTowerEvent;
    public event Action SellTowerEvent;

    public event Action PointerEnterEvent;
    public event Action PointerExitEvent;

    public void Initialize()
    {
        _buildingView.TowerButtons.ForEach(click => click.ClickEvent += OnBuildTower);
        _buildingView.TowerButtons.ForEach(enter => enter.PointerEnterEvent += OnSelectPreviewTower);
        _buildingView.TowerButtons.ForEach(exit => exit.PointerExitEvent += OnDeselectPreviewTower);

        _towerMenu.UpgradeButton.ClickEvent += OnUpgradeTower;
        _towerMenu.UpgradeButton.PointerEnterEvent += OnPointerEnterButton;
        _towerMenu.UpgradeButton.PointerExitEvent += OnPointerExitButton;

        _towerMenu.SellButton.ClickEvent += OnSellTower;
        _towerMenu.SellButton.PointerEnterEvent += OnPointerEnterButton;
        _towerMenu.SellButton.PointerExitEvent += OnPointerExitButton;

        SetConfig();
        HideMenus();
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

    public void HideMenus()
    {
        _buildingView.Hide();
        _towerMenu.Hide();
    }

    private void SetConfig()
    {
        foreach (var button in _buildingView.TowerButtons)
        {
            button.SetCost(_towerFactory.GetConfig(button.Type).CostToBuild);
        }
    }

    private void OnSelectPreviewTower(TowerType type)
    {
        SelectPreviewTowerEvent?.Invoke(type);
    }

    private void OnDeselectPreviewTower(TowerType type)
    {
        DeselectPreviewTowerEvent?.Invoke(type);
    }

    private void OnBuildTower(TowerType type)
    {
        BuildTowerEvent?.Invoke(type);
    }

    private void OnUpgradeTower()
    {
        UpgradeTowerEvent?.Invoke();
    }

    private void OnSellTower()
    {
        SellTowerEvent?.Invoke();
    }

    private void OnPointerEnterButton()
    {
        PointerEnterEvent?.Invoke();
    }

    private void OnPointerExitButton()
    {
        PointerExitEvent?.Invoke();
    }
}
