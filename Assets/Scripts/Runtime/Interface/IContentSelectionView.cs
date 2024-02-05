public interface IContentSelectionView
{
    void OnBuildTower(TowerType type);
    void OnSellSelectedTower();
    void OnUpgradeTower();

    void OnShowPreviewTower(TowerType type);
    void OnHidePreviewTower(TowerType type);
    void OnPointerOverButton();
    void OnPointerOutButton();
}
