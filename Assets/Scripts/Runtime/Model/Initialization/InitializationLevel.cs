using UnityEngine;

public class InitializationLevel : MonoBehaviour
{
    [Header("Models")]
    [SerializeField] private TowerFactory _towerFactory;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private TowerController _towerController;
    [SerializeField] private RaycastController _raycastController;
    [SerializeField] private ActionMapReader _actionMapReader;
    [SerializeField] private TilemapController _tilemapController;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private GameCycle _gamecycle;
    [SerializeField] private Health _health;
    [SerializeField] private Coins _coins;
    [Header("Views"), Space]
    [SerializeField] private ContentSelectionView _contentSelectionView;
    [SerializeField] private GameplayButtonsView _gameplayButtonsView;
    [SerializeField] private LevelConfigProvider _levelConfigProvider;
    [SerializeField] private PathPointsView _pathPointsView;
    [SerializeField] private HealthView _healthView;
    [SerializeField] private CoinsView _coinsView;
    [Header("Presenters"), Space]
    [SerializeField] private ScorePresenter _scorePresenter;
    [SerializeField] private GameplayPresenter _gameplayPresenter;

    private void Awake()
    {
        var config = _levelConfigProvider.Get();

        _actionMapReader.Initialize();
        _raycastController.Initialize(_actionMapReader);
        _cameraController.Initialize(_actionMapReader);
        _tilemapController.Initialize();
        _towerController.Initialize(_towerFactory);
        _gamecycle.Initialize(config.PrepareTime, _contentSelectionView);

        _scorePresenter.Initialize(_healthView, _coinsView, _health, _coins);
        _gameplayPresenter.Initialize(_gameplayButtonsView, _contentSelectionView, _towerController, _pathPointsView, _tilemapController, _raycastController, _coins);

        _gameplayButtonsView.Initialize();
        _gameplayButtonsView.SetCost(_towerFactory.GetAllCostTowers());
        _contentSelectionView.Initialize(_actionMapReader, _towerFactory);
        _health.Initialize(config.Health);
        _coins.Initialize(config.Coins);
    }
}
