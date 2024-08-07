using System;

namespace MicroTowerDefence
{
    public interface ITowerController
    {
        event Action<uint, uint> OnTowerCostEvent;
    }
}