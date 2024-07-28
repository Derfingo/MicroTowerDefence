using System;

namespace MicroTowerDefence
{
    public interface IStart
    {
        event Action OnStartEvent;
        void Reset();
    }
}