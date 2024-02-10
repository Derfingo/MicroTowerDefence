using System;

public interface ICoins
{
    public event Action<uint> UpdateCoinsEvent;
    bool Check(uint cost);
}
