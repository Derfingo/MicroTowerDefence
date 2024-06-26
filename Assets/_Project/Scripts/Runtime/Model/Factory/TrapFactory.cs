using UnityEngine;

namespace MicroTowerDefence
{
    public class TrapFactory : GameObjectFactory
    {
        [SerializeField] private TrapBase _arrow;
        [SerializeField] private TrapBase _spike;
    }

    public enum TrapType
    {
        Spike,
        Arrow
    }
}