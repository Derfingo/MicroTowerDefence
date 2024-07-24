using System;

namespace MicroTowerDefence
{
    public interface IHealth
    {
        public event Action<uint> OnChangeHealthEvent;
    }
}