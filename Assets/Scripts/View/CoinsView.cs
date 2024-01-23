using DG.Tweening;
using TMPro;
using UnityEngine;

public class CoinsView : ViewBase
{
    [SerializeField] private TextMeshProUGUI _coinsAmount;
    [SerializeField] private Transform _coinModel;

    public void SetCoins(uint amount)
    {
        _coinsAmount.text = amount.ToString();
    }

    public void Animate()
    {
        _coinModel.DOLocalRotate(new Vector3(0f, -180f, 0f), 0.8f, RotateMode.WorldAxisAdd).SetEase(Ease.InOutBack);
    }
}
