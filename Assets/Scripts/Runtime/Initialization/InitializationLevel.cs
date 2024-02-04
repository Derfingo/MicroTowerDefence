using UnityEngine;

public class InitializationLevel : MonoBehaviour
{
    [SerializeField] private TowerFactory _towerFactory;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private TowerController _buildingController;
    [SerializeField] private ContentSelectionView _contentSelector;
    [SerializeField] private RaycastController _raycastController;
    [SerializeField] private ActionMapReader _actionMapReader;
    [SerializeField] private TilemapController _tilemapController;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private GameCycle _gamecycle;
    [SerializeField] private Health _health;
    [SerializeField] private Coins _coins;
    [Space]
    [SerializeField] private GameplayViewController _gameplayViewController;
    [Space]
    [SerializeField] private LevelConfigProvider _levelConfigProvider;

    private void Awake()
    {
        var config = _levelConfigProvider.Get();

        _actionMapReader.Initialize();
        _raycastController.Initialize(_actionMapReader);
        _cameraController.Initialize(_actionMapReader);
        _tilemapController.Initialize();
        _contentSelector.Initialize(_towerFactory, _actionMapReader);
        _buildingController.Initialize(_towerFactory);
        _gamecycle.Initialize(config.PrepareTime);

        _gameplayViewController.Initialize();
        _health.Initialize(config.Health);
        _coins.Initialize(config.Coins);
    }
}
