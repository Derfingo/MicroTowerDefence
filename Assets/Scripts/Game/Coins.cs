using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField] private uint _initialConins;
    [SerializeField] private CoinsView _view;

    private uint _coins;

    private void Start()
    {
        _coins = _initialConins;
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
        _coins = _initialConins;
        _view.SetCoins(_coins);
    }
}
