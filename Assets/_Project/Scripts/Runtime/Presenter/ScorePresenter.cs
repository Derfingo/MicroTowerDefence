using System;

namespace MicroTowerDefence
{
    public class ScorePresenter : IDisposable
    {
        private readonly IHealthView _healthView;
        private readonly ICoinsView _coinsView;
        private readonly IHealth _health;
        private readonly ICoins _coins;

        public ScorePresenter(IHealthView healthView, ICoinsView coinsView, IHealth health, ICoins coins)
        {
            _healthView = healthView;
            _coinsView = coinsView;
            _health = health;
            _coins = coins;

            _health.OnChangeHealthEvent += _healthView.UpdateHealth;
            _coins.UpdateCoinsEvent += _coinsView.UpdateCoins;
        }

        public void Dispose()
        {
            _health.OnChangeHealthEvent -= _healthView.UpdateHealth;
            _coins.UpdateCoinsEvent -= _coinsView.UpdateCoins;
        }
    }
}