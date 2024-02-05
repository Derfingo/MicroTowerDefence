using System;

public interface ICoinsModel
{
    public event Action<uint> UpdateCoinsEvent;
}
