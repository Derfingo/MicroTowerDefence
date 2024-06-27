using UnityEngine;
using Zenject;

namespace MicroTowerDefence
{
    public class ScorePresenter : MonoBehaviour
    {
        private IHealthView _healthView;
        private ICoinsView _coinsView;

        private IHealth _health;
        private ICoins _coins;

        [Inject]
        public void Initialize(IHealthView healthView, ICoinsView coinsView, IHealth health, ICoins coins)
        {
            _healthView = healthView;
            _coinsView = coinsView;

            _health = health;
            _coins = coins;

            _health.UpdateHealthEvent += _healthView.UpdateHealth;
            _coins.UpdateCoinsEvent += _coinsView.UpdateCoins;
        }
    }
}