using System;
using UnityEngine;

public class GameplayViewController : ViewBase
{
    [SerializeField] private TowerMenu _towerMenu;
    [SerializeField] private BuildingView _buildingView;
    [SerializeField] private TowerFactory _towerFactory;

    public event Action<TowerType> OnSelectBuilding;

    public event Action OnUpgradeBuilding;
    public event Action OnSellBuilding;

    private void Start()
    {
        _buildingView.Buttons.ForEach(b => b.AddListener(OnSelectedBuilding));
        _towerMenu.UpgradeButton.onClick.AddListener(OnUpgradeTowerClicked);
        _towerMenu.SellButton.onClick.AddListener(OnSellTowerCliked);

        SetCofig();
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

    private void SetCofig()
    {
        foreach (var button in _buildingView.Buttons)
        {
            button.SetCost(_towerFactory.GetConfig(button.Type).Get(0).Cost);
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
