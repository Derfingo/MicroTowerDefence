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

    public void AddCoinsAnimate()
    {
        _coinModel.DOLocalRotate(new Vector3(0f, -180f, 0f), 0.8f, RotateMode.WorldAxisAdd).SetEase(Ease.InOutBack);
    }

    public void DeficiencyCoinsAnimate()
    {
        _coinModel.DOShakeRotation(0.2f, 50f, 10, 30f).SetEase(Ease.InFlash);
    }
}
