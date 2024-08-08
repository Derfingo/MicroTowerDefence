using System;

namespace MicroTowerDefence
{
    public interface ITowerInteractionView
    {
        event Action OnSellTowerEvent;
        event Action OnUpgradeTowerEvent;

        event Action OnEnterButtonEvent;
        event Action OnExitButtonEvent;

        void SetCosts(uint upgradeCost, uint sellCost);
        void Show();
        void Hide();
    }
}
