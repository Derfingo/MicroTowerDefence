using System;
using System.Threading.Tasks;

namespace MicroTowerDefence
{
    public interface ILoadingOperation
    {
        string GetName { get; }

        Task Load(Action<float> onProgress);
    }
}