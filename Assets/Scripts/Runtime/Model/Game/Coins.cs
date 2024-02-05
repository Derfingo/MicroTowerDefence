using System;
using UnityEngine;

public class Coins : MonoBehaviour, ICoinsModel
{
    //[SerializeField] private CoinsView _view;
    public event Action<uint> UpdateCoinsEvent;

    private uint _initialCoins;
    private uint _coins;

    public void Initialize(uint initialCoins)
    {
        _initialCoins = initialCoins;
        _coins = initialCoins;
        //_view.UpdateCoins(_coins);
        UpdateCoinsEvent?.Invoke(_initialCoins);
    }

    public void Add(uint amount)
    {
        _coins += amount;
        //_view.UpdateCoins(_coins);
        UpdateCoinsEvent?.Invoke(_coins);
    }

    public bool Check(uint cost)
    {
        if (_coins < cost)
        {
            Debug.Log("coins is not enough");
            return false;
        }
        
        return true;
    }

    public bool TrySpend(uint cost)
    {
        if (Check(cost))
        {
            _coins -= cost;
            //_view.UpdateCoins(_coins);
            UpdateCoinsEvent?.Invoke(_coins);
            return true;
        }

        return false;
    }

    public void Reset()
    {
        _coins = _initialCoins;
        //_view.UpdateCoins(_coins);
        UpdateCoinsEvent?.Invoke(_coins);
    }
}
