using System;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private TowerMenuUI _towerMenu;
    [SerializeField] private BuildingMenuUI _buildingMenu;
    [SerializeField] private ContentSelection _contentSelection;

    public event Action<GameTileContentType, GameTile> OnBuildClick;
    public event Action<GameTile> OnUpgradeClick;
    public event Action<GameTile> OnSellClick;

    private void Start()
    {
        _buildingMenu.Buttons.ForEach(b => b.AddListener(OnBuildClicked));

        _contentSelection.OnBuildingMenu += ShowBuildingMenu;
        _contentSelection.OnTowerMenu += ShowTowerMenu;
        _contentSelection.OnHideMenu += HideMenus;

        _towerMenu.UpgradeButton.onClick.AddListener(OnUpgradeTowerClicked);
        _towerMenu.SellButton.onClick.AddListener(OnSellTowerCliked);
    }

    private void ShowBuildingMenu()
    {
        HideMenus();
        _buildingMenu.Show();
    }

    private void ShowTowerMenu()
    {
        HideMenus();
        _towerMenu.Show();
    }

    private void HideMenus()
    {
        _buildingMenu.Hide();
        _towerMenu.Hide();
    }

    private void OnBuildClicked(GameTileContentType type)
    {
        OnBuildClick?.Invoke(type, _contentSelection.TempTile);
        HideMenus();
    }

    private void OnUpgradeTowerClicked()
    {
        OnUpgradeClick?.Invoke(_contentSelection.TempTile);
        HideMenus();
    }

    private void OnSellTowerCliked()
    {
        OnSellClick?.Invoke(_contentSelection.TempTile);
        HideMenus();
    }
}
