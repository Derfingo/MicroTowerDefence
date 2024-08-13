using System;
namespace MicroTowerDefence
{
    public interface ILoader
    {
        void LoadAsync(string name, bool isFadeIn = true, Action onComplete = null);
        void LoadNextLevel();
    }
}
