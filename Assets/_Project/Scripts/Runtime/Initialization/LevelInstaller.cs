using UnityEngine;
using Zenject;

namespace MicroTowerDefence
{
    public class LevelInstaller : MonoInstaller
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

        private LevelConfig _config;

        public override void InstallBindings()
        {
            _config = _levelConfigProvider.Get();

            BindModels();
            BindViews();
            BindPresenters();
        }

        private void BindModels()
        {
            Container.BindInstances(_config.PrepareTime,
                                    _pathPointsView.GetConfig());

            Container.BindInterfacesTo<EnemyContorller>().FromInstance(_enemyController).AsSingle();
            Container.BindInterfacesTo<TilemapController>().FromInstance(_tilemapController).AsSingle();
            Container.BindInterfacesTo<Health>().FromInstance(_health).AsSingle();
            Container.BindInterfacesTo<Coins>().FromInstance(_coins).AsSingle();
            Container.BindInstance(_config.Health).WhenInjectedInto<Health>();
            Container.BindInstance(_config.Coins).WhenInjectedInto<Coins>();
            Container.Bind<IInputActions>().FromInstance(_actionMapReader).AsSingle();
            Container.Bind<IRaycast>().FromInstance(_raycastController).AsSingle();
            Container.Bind<ILateUpdate>().FromInstance(_cameraController).AsSingle();
            Container.Bind<ISelection>().FromInstance(_contentSelection).AsSingle();
            Container.Bind<TowerFactory>().FromInstance(_towerFactory).AsSingle();
            Container.BindInterfacesTo<TowerController>().FromInstance(_towerController).AsSingle();
            Container.Bind<GameCycle>().FromInstance(_gamecycle).AsSingle().WithArguments(_config.PrepareTime, _pathPointsView.GetConfig());
        }

        private void BindPresenters()
        {
            Container.Bind<ScorePresenter>().FromInstance(_scorePresenter).AsSingle().NonLazy();
            Container.Bind<GameplayPresenter>().FromInstance(_gameplayPresenter).AsSingle().NonLazy();
            Container.Bind<InteractionPresenter>().FromInstance(_selectionPresenter).AsSingle().NonLazy();
        }

        private void BindViews()
        {
            Container.Bind<IHealthView>().FromInstance(_healthView).AsSingle().NonLazy();
            Container.Bind<ICoinsView>().FromInstance(_coinsView).AsSingle().NonLazy();
            Container.Bind<IGameplayButtonsView>().FromInstance(_gameplayButtonsView).AsSingle().NonLazy();
            Container.BindInterfacesTo<ContentSelectionView>().FromInstance(_contentSelectionView).AsSingle().NonLazy();
            Container.BindInterfacesTo<ProjectileController>().FromInstance(_projectileController).AsSingle().NonLazy();
        }
    }
}