using System;

namespace MicroTowerDefence
{
    public interface IReactiveProperty<T>
    {
        event Action<T> OnChangeEvent;
        T Value { get; }
    }
}
