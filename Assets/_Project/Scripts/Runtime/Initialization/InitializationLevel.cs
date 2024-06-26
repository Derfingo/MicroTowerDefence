using UnityEngine;

namespace MicroTowerDefence
{
    public class InitializationLevel : MonoBehaviour
    {
        [Header("Models")]
        [SerializeField] private ProjectileController _projectileController;
        [SerializeField] private EnemyContorller _enemyController;
        [SerializeField] private TowerFactory _towerFactory;
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private TowerController _towerController;
        [SerializeField] private RaycastController _raycastController;
        [SerializeField] private ActionMapReader _actionMapReader;
        [SerializeField] private TilemapController _tilemapController;
        [SerializeField] private ContentSelection _contentSelection;
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
        [SerializeField] private InteractionPresenter _selectionPresenter;

        private void Awake()
        {
            var config = _levelConfigProvider.Get();

            _actionMapReader.Initialize();
            _raycastController.Initialize(_actionMapReader);
            _cameraController.Initialize(_actionMapReader);
            _tilemapController.Initialize();
            _towerController.Initialize(_towerFactory);
            _contentSelection.Initialize(_actionMapReader);
            _gamecycle.Initialize(config.PrepareTime, _actionMapReader, GetResets(), GetUpdates(), GetLateUpdates(), _pathPointsView.GetConfig());

            _scorePresenter.Initialize(_healthView, _coinsView, _health, _coins);
            _selectionPresenter.Initialize(_contentSelection,
                                           _tilemapController,
                                           _raycastController,
                                           _contentSelectionView);

            _gameplayButtonsView.Initialize();
            _gameplayButtonsView.SetCost(_towerFactory.GetAllCostTowers());
            _contentSelectionView.Initialize(_actionMapReader);
            _health.Initialize(config.Health);
            _coins.Initialize(config.Coins);
        }

        private IUpdate[] GetUpdates()
        {
            var updates = new IUpdate[]
            {
            _contentSelectionView,
            _projectileController,
            _tilemapController,
            _towerController,
            _enemyController,
            };

            return updates;
        }

        private ILateUpdate[] GetLateUpdates()
        {
            var lateUpdates = new ILateUpdate[]
            {
            _cameraController
            };

            return lateUpdates;
        }

        private IReset[] GetResets()
        {
            var resets = new IReset[]
            {
            _projectileController,
            _towerController,
            _enemyController,
            _health,
            _coins,
            };

            return resets;
        }
    }
}