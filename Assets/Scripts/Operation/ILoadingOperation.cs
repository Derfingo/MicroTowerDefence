using System;
using System.Threading.Tasks;

public interface ILoadingOperation
{
    string GetName { get; }

    Task Load(Action<float> onProgress);
}
