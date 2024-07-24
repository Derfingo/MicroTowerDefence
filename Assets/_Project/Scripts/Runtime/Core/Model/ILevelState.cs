using System;

namespace MicroTowerDefence
{
    public interface ILevelState
    {
        event Action OnWinEvent;
        event Action OnPauseEvent;
        event Action OnDefeatEvent;
    }
}
