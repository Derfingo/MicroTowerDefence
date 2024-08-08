using UnityEngine;
using Zenject;

namespace MicroTowerDefence
{
    public class LevelInstaller : MonoInstaller, IInitializable
    {
        [Header("Setup")]
        [SerializeField] private LevelConfigProvider _levelConfigProvider;
        [SerializeField] private ProjectileFactory _projectileFactory;
        [SerializeField] private TowerFactory _towerFactory;
        [SerializeField] private GameScenario _gameScenario;
        [Space]
        [Header("Model")]
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private RaycastController _raycastController;
        [SerializeField] private TilemapController _tilemapController;
        [SerializeField] private LevelState _levelState;
        [SerializeField] private LevelCycle _levelCycle;
        [Space]
        [Header("View")]
        [SerializeField] private TowerInterationView _towerInterationView;
        [SerializeField] private TowerBuildingView _towerBuildingView;
        [SerializeField] private ReadyToStartView _readyToStartView;
        [SerializeField] private PathPointsView _pathPointsView;
        [SerializeField] private CursorView _cursorView;
        [SerializeField] private HealthView _healthView;
        [SerializeField] private StateView _stateView;
        [SerializeField] private CoinsView _coinsView;

        private LevelConfig _config;

        public void Initialize()
        {
            ResolveModelView();
        }

        private void ResolveModelView()
        {
        }

        public override void InstallBindings()
        {
            _config = _levelConfigProvider.Get();

            BindModels();
            BindViews();
            BindPresenters();
            BindViewModels();
        }

        private void BindViewModels()
        {
            Container.BindInterfacesAndSelfTo<CursorViewModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<TowerBuildingViewModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<TowerInteractionViewModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<TowerMenuTransitionViewModel>().AsSingle().NonLazy();
        }

        private void BindModels()
        {
            BindInput();
            BindFactory();
            BindScore();
            BindControllers();
            BindScenario();
            BindLevelCycle();
            BindLevelState();
        }

        private void BindLevelState()
        {
            Container.BindInterfacesAndSelfTo<LevelState>().FromInstance(_levelState).AsSingle();
            Container.BindInstance(_config.PrepareTime).WhenInjectedInto<LevelState>();
            Container.BindInstance(_pathPointsView.GetConfig()).WhenInjectedInto<LevelState>();
        }

        private void BindInput()
        {
            Container.Bind<IInputActions>().To<ActionMapReader>().AsSingle();
        }

        private void BindFactory()
        {
            Container.BindInterfacesAndSelfTo<TowerFactory>().FromInstance(_towerFactory).AsSingle();
            Container.Bind<ProjectileFactory>().FromInstance(_projectileFactory).AsSingle();
        }

        private void BindScore()
        {
            Container.BindInterfacesAndSelfTo<Health>().AsSingle().WithArguments(_config.Health);
            Container.BindInterfacesAndSelfTo<Coins>().AsSingle().WithArguments(_config.Coins);
        }

        private void BindScenario()
        {
            Container.Bind<GameScenario>().FromInstance(_gameScenario).AsSingle();
        }

        private void BindLevelCycle()
        {
            Container.Bind<LevelCycle>().FromInstance(_levelCycle).AsSingle();
        }

        private void BindControllers()
        {
            Container.BindInterfacesAndSelfTo<EnemyContorller>().AsSingle().WithArguments(_pathPointsView.GetConfig());
            Container.BindInterfacesAndSelfTo<TilemapController>().FromInstance(_tilemapController).AsSingle();
            Container.BindInterfacesAndSelfTo<RaycastController>().FromInstance(_raycastController).AsSingle();
            Container.BindInterfacesAndSelfTo<ProjectileController>().AsSingle();
            Container.BindInterfacesAndSelfTo<TowerController>().AsSingle();
            Container.BindInterfacesAndSelfTo<ContentSelection>().AsSingle();
            Container.Bind<ILateUpdate>().FromInstance(_cameraController).AsSingle();
            Container.BindInterfacesAndSelfTo<TowerPreview>().AsSingle().NonLazy();
        }

        private void BindPresenters()
        {
            Container.Bind<ScorePresenter>().AsSingle().NonLazy();
            Container.Bind<StateLevelPresenter>().AsSingle().NonLazy();
        }

        private void BindViews()
        {
            Container.Bind<IHealthView>().FromInstance(_healthView).AsSingle();
            Container.Bind<ICoinsView>().FromInstance(_coinsView).AsSingle();
            Container.BindInterfacesAndSelfTo<ReadyToStartView>().FromInstance(_readyToStartView).AsSingle();
            Container.BindInterfacesAndSelfTo<StateView>().FromInstance(_stateView).AsSingle();
            Container.BindInterfacesAndSelfTo<CursorView>().FromInstance(_cursorView).AsSingle();
            Container.BindInterfacesAndSelfTo<TowerBuildingView>().FromInstance(_towerBuildingView).AsSingle();
            Container.BindInterfacesAndSelfTo<TowerInterationView>().FromInstance(_towerInterationView).AsSingle();
        }

        private void OnDestroy()
        {
            Container.Resolve<IInputActions>().Dispose();
            Container.UnbindAll();
        }
    }
}