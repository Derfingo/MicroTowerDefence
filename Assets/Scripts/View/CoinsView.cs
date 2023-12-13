using TMPro;
using UnityEngine;

public class CoinsView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinsAmount;

    public void SetCoins(uint amount)
    {
        _coinsAmount.text = amount.ToString();
    }
}
