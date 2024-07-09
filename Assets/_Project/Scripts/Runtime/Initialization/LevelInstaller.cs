using UnityEngine;
using Zenject;

namespace MicroTowerDefence
{
    public class LevelInstaller : MonoInstaller
    {
        [Header("Setup")]
        [SerializeField] private ProjectileFactory _projectileFactory;
        [SerializeField] private TowerFactory _towerFactory;
        [SerializeField] private GameScenario _gameScenario;
        [Space]
        [Header("Model")]
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private RaycastController _raycastController;
        [SerializeField] private TilemapController _tilemapController;
        [SerializeField] private GameCycle _gamecycle;
        [Space]
        [Header("View")]
        [SerializeField] private ContentSelectionView _contentSelectionView;
        [SerializeField] private GameplayButtonsView _gameplayButtonsView;
        [SerializeField] private LevelConfigProvider _levelConfigProvider;
        [SerializeField] private PathPointsView _pathPointsView;
        [SerializeField] private HealthView _healthView;
        [SerializeField] private CoinsView _coinsView;

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
            BindInput();
            BindFactory();
            BindScore();
            BindControllers();
            BindScenario();
            BindGameCycle();
        }

        private void BindInput()
        {
            Container.Bind<IInputActions>().To<ActionMapReader>().AsSingle();
        }

        private void BindFactory()
        {
            Container.Bind<TowerFactory>().FromInstance(_towerFactory).AsSingle();
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

        private void BindGameCycle()
        {
            Container.Bind<GameCycle>().FromInstance(_gamecycle).AsSingle();
            Container.BindInstance(_config.PrepareTime).WhenInjectedInto<GameCycle>();
            Container.BindInstance(_pathPointsView.GetConfig()).WhenInjectedInto<GameCycle>();
        }

        private void BindControllers()
        {
            Container.BindInterfacesAndSelfTo<TowerController>().AsSingle();
            Container.BindInterfacesAndSelfTo<ProjectileController>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyContorller>().AsSingle();
            Container.BindInterfacesAndSelfTo<TilemapController>().FromInstance(_tilemapController).AsSingle();
            Container.BindInterfacesAndSelfTo<ContentSelection>().AsSingle();
            Container.BindInterfacesAndSelfTo<RaycastController>().FromInstance(_raycastController).AsSingle();
            Container.Bind<ILateUpdate>().FromInstance(_cameraController).AsSingle();
        }

        private void BindPresenters()
        {
            Container.Bind<ScorePresenter>().AsSingle().NonLazy();
            Container.Bind<InteractionPresenter>().AsSingle().NonLazy();
        }

        private void BindViews()
        {
            Container.Bind<IHealthView>().FromInstance(_healthView).AsSingle();
            Container.Bind<ICoinsView>().FromInstance(_coinsView).AsSingle();
            Container.Bind<IGameplayButtonsView>().FromInstance(_gameplayButtonsView).AsSingle();
            Container.BindInterfacesTo<ContentSelectionView>().FromInstance(_contentSelectionView).AsSingle();
        }
    }
}