using System;
using System.Collections.Generic;
using UnityEngine;

public class GameplayButtonsView : ViewBase, IGameplayButtonsView
{
    [SerializeField] private TowerButtonsView _towerButtonsView;
    [SerializeField] private BuildTowerButtonsView _buildTowerButtonsView;

    public event Action<TowerType> BuildTowerEvent;
    public event Action<TowerType> EnterPreviewTowerEvent;
    public event Action<TowerType> ExitPreviewTowerEvent;

    public event Action UpgradeTowerEvent;
    public event Action SellTowerEvent;

    public event Action PointerEnterEvent;
    public event Action PointerExitEvent;

    private ViewBase _currentView;

    public void Initialize()
    {
        _buildTowerButtonsView.TowerButtons.ForEach(click => click.ClickEvent += OnBuildTower);
        _buildTowerButtonsView.TowerButtons.ForEach(enter => enter.PointerEnterEvent += OnEnterPreviewTower);
        _buildTowerButtonsView.TowerButtons.ForEach(exit => exit.PointerExitEvent += OnExitPreviewTower);

        _towerButtonsView.UpgradeButton.ClickEvent += OnUpgradeTower;
        _towerButtonsView.UpgradeButton.PointerEnterEvent += OnPointerEnter;
        _towerButtonsView.UpgradeButton.PointerExitEvent += OnPointerExit;

        _towerButtonsView.SellButton.ClickEvent += OnSellTower;
        _towerButtonsView.SellButton.PointerEnterEvent += OnPointerEnter;
        _towerButtonsView.SellButton.PointerExitEvent += OnPointerExit;
        
        _currentView = _buildTowerButtonsView;
        HideButtonViews();
    }

    // state views

    public void ShowBuildTowerView()
    {
        _currentView.Hide();
        _currentView = _buildTowerButtonsView;
        _currentView.Show();
    }

    public void ShowTowerView(uint upgradeCost, uint sellCost)
    {
        _currentView.Hide();
        _towerButtonsView.SetCosts(upgradeCost, sellCost);
        _currentView = _towerButtonsView;
        _towerButtonsView.Show();
    }

    public void HideButtonViews()
    {
        _buildTowerButtonsView.Hide();
        _towerButtonsView.Hide();
    }

    // cost config

    public void SetCost(Dictionary<TowerType, uint> costTowers)
    {
        if (_buildTowerButtonsView.TowerButtons == null)
        {
            Debug.Log("null");
        }

        foreach (var button in _buildTowerButtonsView.TowerButtons)
        {
            if (costTowers.TryGetValue(button.Type, out var cost))
            {
                button.SetCost(cost);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }

    // button click

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

    // state pointer on button

    private void OnEnterPreviewTower(TowerType type)
    {
        EnterPreviewTowerEvent?.Invoke(type);
    }

    private void OnExitPreviewTower(TowerType type)
    {
        ExitPreviewTowerEvent?.Invoke(type);
    }

    private void OnPointerEnter()
    {
        PointerEnterEvent?.Invoke();
    }

    private void OnPointerExit()
    {
        PointerExitEvent?.Invoke();
    }
}
