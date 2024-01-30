using UnityEngine;

public class Coins : ViewBase
{
    [SerializeField] private CoinsView _view;

    private uint _initialCoins;
    private uint _coins;

    public void Initialize(uint initialCoins)
    {
        _initialCoins = initialCoins;
        _coins = initialCoins;
        _view.SetCoins(_coins);
    }

    public void Add(uint amount)
    {
        _coins += amount;
        _view.SetCoins(_coins);
        _view.AddCoinsAnimate();
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
            _view.SetCoins(_coins);
            return true;
        }

        return false;
    }

    public void Reset()
    {
        _coins = _initialCoins;
        _view.SetCoins(_coins);
    }
}
