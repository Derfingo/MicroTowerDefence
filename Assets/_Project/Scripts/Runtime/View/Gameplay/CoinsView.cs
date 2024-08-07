using DG.Tweening;
using TMPro;
using UnityEngine;

namespace MicroTowerDefence
{
    public class CoinsView : ViewBase, ICoinsView
    {
        [SerializeField] private TextMeshProUGUI _coinsAmount;
        [SerializeField] private Transform _coinModel;

        public void UpdateCoins(uint coins)
        {
            _coinsAmount.text = coins.ToString();
            AddCoinsAnimate();
        }

        private void AddCoinsAnimate()
        {
            _coinModel.DOLocalRotate(new Vector3(0f, -180f, 0f), 0.4f, RotateMode.WorldAxisAdd).SetEase(Ease.InOutBack);
        }
    }
}