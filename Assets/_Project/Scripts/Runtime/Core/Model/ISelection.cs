using System;

namespace MicroTowerDefence
{
    public interface ISelection
    {
        public event Action OnCancelSelectedEvent;
        public event Action SelectToBuildEvent;

        void OnBuildTower(TowerType type);
        void OnSellTower();
        void OnUpgradeTower();

        void OnShowPreview(TowerType type);
        void OnHidePreview(TowerType type);
    }
}