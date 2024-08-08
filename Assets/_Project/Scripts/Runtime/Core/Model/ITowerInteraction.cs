using System;

namespace MicroTowerDefence
{
    public interface ITowerInteraction
    {
        event Action<uint, uint> OnTowerCostEvent;
    }
}