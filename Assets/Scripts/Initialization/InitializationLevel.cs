using UnityEngine;

public class InitializationLevel : MonoBehaviour
{
    [SerializeField] private TowerFactory _towerFactory;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private BuildingController _buildingController;
    [SerializeField] private ContentSelectionView _contentSelector;
    [SerializeField] private RaycastController _raycastController;
    [SerializeField] private ActionMapReader _actionMapReader;
    [SerializeField] private TilemapController _tilemapController;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private GameCycle _gamecycle;
    [Space]
    [SerializeField] private GameplayViewController _gameplayViewController;

    private void Awake()
    {
        _raycastController.Initialize(_inputReader);
        _cameraController.Initialize(_inputReader);
        _tilemapController.Initialize();
        _contentSelector.Initialize(_towerFactory, _inputReader);
        _buildingController.Initialize(_towerFactory);
        _gamecycle.Initialize();
    }

    private void Start()
    {
        _gameplayViewController.Initialize();
    }
}
