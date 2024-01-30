using DG.Tweening;
using TMPro;
using UnityEngine;

public class HealthView : ViewBase
{
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private Transform _healthModel;

    public void SetHealth(uint health)
    {
        _healthText.text = health.ToString();
    }

    public void Animate()
    {
        _healthModel.DOShakeRotation(0.5f, 50f, 10, 30f).SetEase(Ease.InFlash);
    }
}
