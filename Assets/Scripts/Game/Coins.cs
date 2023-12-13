using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField] private CoinsView _view;

    private uint _coins;

    private void Start()
    {
        _coins = 100;
        _view.SetCoins(_coins);
    }

    public void Add(uint amount)
    {
        _coins += amount;
        _view.SetCoins(_coins);
    }

    public void Spend(uint cost)
    {
        if (Check(cost))
        {
            _coins -= cost;
            _view.SetCoins(_coins);
        }
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
}
