using System;

namespace MicroTowerDefence
{
    public class MainViewModel : IReactiveProperty<string>
    {
        public event Action<string> OnChangeEvent;
        public string Value => throw new NotImplementedException();
    }
}
