using System;
namespace MicroTowerDefence
{
    public interface ILoader
    {
        void LoadAsync(string name, Action onComplete = null);
    }
}
