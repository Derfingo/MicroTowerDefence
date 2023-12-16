using TMPro;
using UnityEngine;

public class CoinsView : ViewBase
{
    [SerializeField] private TextMeshProUGUI _coinsAmount;

    public void SetCoins(uint amount)
    {
        _coinsAmount.text = amount.ToString();
    }
}
