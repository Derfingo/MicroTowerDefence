using System;
using System.Collections.Generic;

namespace MicroTowerDefence
{
    public interface ITowerBuildingView
    {
        event Action<TowerType> BuildTowerEvent;
        event Action<TowerType> EnterPreviewTowerEvent;
        event Action<TowerType> ExitPreviewTowerEvent;

        void SetTowersCost(Dictionary<TowerType, uint> costTowers);
        void EnableButtons(bool isEnable);
        void Show();
        void Hide();
    }
}
