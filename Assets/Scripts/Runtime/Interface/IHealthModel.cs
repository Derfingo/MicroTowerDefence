using System;

public interface IHealthModel
{
    public event Action<uint> UpdateHealthEvent;
}
