using System;

namespace MicroTowerDefence
{
    public interface ILevelState
    {
        event Action<bool> OnPauseEvent;
        event Action OnDefeatEvent;
        event Action OnWinEvent;

        void OnPause();
        void OnRestart();
        void OnMainMenu();
        void OnNextLevel();
    }
}
