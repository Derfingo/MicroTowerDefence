using System;

namespace MicroTowerDefence
{
    public interface IStateView
    {
        event Action OnNextLevelEvent;
        event Action OnContinueEvent;
        event Action OnMainMenuEvent;
        event Action OnSettingsEvent;
        event Action OnRestartEvent;

        void ShowDefeatMenu();
        void ShowPauseMenu(bool isEnable);
        void ShowWinMenu();
        void HideMenus();
    }
}
