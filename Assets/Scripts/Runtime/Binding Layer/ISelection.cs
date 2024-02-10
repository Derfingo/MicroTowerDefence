using System;

public interface ISelection
{
    public event Action<bool> SelectedContentEvent;
    public event Action<uint, uint> ShowTowerMenuEvent;
    public event Action SelectToBuildEvent;
    public event Action CancelSelectedEvent;

    void OnBuildTower(TowerType type);
    void OnSellTower();
    void OnUpgradeTower();

    void OnShowPreview(TowerType type);
    void OnHidePreview(TowerType type);
}