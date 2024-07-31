using System;
using System.Collections.Generic;

namespace MicroTowerDefence
{
    public class ReactiveProperty<T> : IReactiveProperty<T>
    {
        public event Action<T> OnChangeEvent = delegate { };

        private static readonly EqualityComparer<T> _comparer = EqualityComparer<T>.Default;
        private T _value = default;

        public T Value
        {
            get => _value;
            set
            {
                if(_comparer.Equals(value, _value) == false)
                {
                    _value = value;
                    OnChangeEvent?.Invoke(value);
                }
            }
        }
    }
}
