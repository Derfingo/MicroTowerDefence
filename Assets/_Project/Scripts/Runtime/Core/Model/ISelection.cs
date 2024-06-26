using System;

namespace MicroTowerDefence
{
    public interface ISelection
    {
        public event Action<bool> SelectedEvent;
        public event Action<bool> CancelSelectedEvent;
        public event Action<uint, uint> ShowTowerMenuEvent;
        public event Action SelectToBuildEvent;

        void OnBuildTower(TowerType type);
        void OnSellTower();
        void OnUpgradeTower();

        void OnShowPreview(TowerType type);
        void OnHidePreview(TowerType type);
    }
}