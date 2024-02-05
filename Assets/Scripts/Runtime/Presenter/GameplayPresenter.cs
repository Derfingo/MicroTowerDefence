using UnityEngine;

public class GameplayPresenter : MonoBehaviour
{
    private IGameplayButtonsView _gameplayButtonsView;
    private IContentSelectionView _contentSelectionView;
    private IHealthView _healthView;
    private ICoinsView _coinsView;
    private IPathView _pathView;

    private IHealthModel _healthModel;
    private ICoinsModel _coinsModel;

    public void Initialize(IGameplayButtonsView gameplayButtonsView,
                           IContentSelectionView contentSelectionModel,
                           IHealthView healthView,
                           ICoinsView coinsView,
                           IHealthModel healthModel,
                           ICoinsModel coinsModel,
                           IPathView pathView)
    {
        _gameplayButtonsView = gameplayButtonsView;
        _healthView = healthView;
        _coinsView = coinsView;

        _contentSelectionView = contentSelectionModel;
        _healthModel = healthModel;
        _coinsModel = coinsModel;

        _healthModel.UpdateHealthEvent += _healthView.UpdateHealth;
        _coinsModel.UpdateCoinsEvent += _coinsView.UpdateCoins;
    }
}
