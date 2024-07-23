using System;

namespace MicroTowerDefence
{
    public interface IRaycast
    {
        public event Action<bool> OnGround;
        bool SetOverUI(bool isUI);
    }
}