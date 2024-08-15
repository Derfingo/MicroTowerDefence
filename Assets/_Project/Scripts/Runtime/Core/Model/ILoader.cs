using System;
using System.Threading.Tasks;
namespace MicroTowerDefence
{
    public interface ILoader
    {
        Task LoadAsync(string name, bool isFadeIn = true);
        Task LoadAddressableAsync(string name, bool isFadeIn = true);
        Task LoadNextLevel();
    }
}
