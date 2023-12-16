using System;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : ViewBase
{
    [SerializeField] private TowerMenu _towerMenu;
    [SerializeField] private BuildingView _buildingView;
    [Space]
    [SerializeField] private List<BuildTowerButton> _towerButtons;

    public event Action<TowerType> OnSelectBuilding;

    public event Action OnUpgradeBuilding;
    public event Action OnSellBuilding;

    private void Start()
    {
        _buildingView.Buttons.ForEach(b => b.AddListener(OnSelectedBuilding));
        _towerMenu.UpgradeButton.onClick.AddListener(OnUpgradeTowerClicked);
        _towerMenu.SellButton.onClick.AddListener(OnSellTowerCliked);
    }

    public void ShowBuildingMenu()
    {
        HideMenus();
        _buildingView.Show();
    }

    public void ShowTowerMenu()
    {
        HideMenus();
        _towerMenu.Show();
    }

    private void HideMenus()
    {
        _buildingView.Hide();
        _towerMenu.Hide();
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
