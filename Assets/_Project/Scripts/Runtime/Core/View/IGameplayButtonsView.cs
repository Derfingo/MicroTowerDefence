using System;
using System.Collections.Generic;

namespace MicroTowerDefence
{
    public interface IGameplayButtonsView
    {
        public event Action<TowerType> BuildTowerEvent;
        public event Action<TowerType> EnterPreviewTowerEvent;
        public event Action<TowerType> ExitPreviewTowerEvent;

        public event Action UpgradeTowerEvent;
        public event Action SellTowerEvent;

        public event Action PointerEnterEvent;
        public event Action PointerExitEvent;

        void SetCost(Dictionary<TowerType, uint> costTowers);

        void HideButtonViews();
    }
}