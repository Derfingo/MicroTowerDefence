using UnityEngine;

public class ScorePresenter : MonoBehaviour
{
    private IHealthView _healthView;
    private ICoinsView _coinsView;

    private IHealthModel _health;
    private ICoinsModel _coins;

    public void Initialize(IHealthView healthView, ICoinsView coinsView, IHealthModel health, ICoinsModel coins)
    {
        _healthView = healthView;
        _coinsView = coinsView;

        _health = health;
        _coins = coins;

        _health.UpdateHealthEvent += _healthView.UpdateHealth;
        _coins.UpdateCoinsEvent += _coinsView.UpdateCoins;
    }
}
