using DG.Tweening;
using TMPro;
using UnityEngine;

namespace MicroTowerDefence
{
    public class HealthView : ViewBase, IHealthView
    {
        [SerializeField] private TextMeshProUGUI _healthText;
        [SerializeField] private Transform _healthModel;

        public void UpdateHealth(uint value)
        {
            _healthText.text = value.ToString();
            Animate();
        }

        private void Animate()
        {
            _healthModel.DOShakeRotation(0.5f, 50f, 10, 30f).SetEase(Ease.InFlash);
        }
    }
}