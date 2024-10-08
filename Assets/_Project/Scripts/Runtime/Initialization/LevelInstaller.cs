using System;
using UnityEngine;
using Zenject;

namespace MicroTowerDefence
{
    public class LevelInstaller : MonoInstaller
    {
        [Header("Setup")]
        [SerializeField] private EnemyWavesConfig _enemyWavesConfig;
        [SerializeField] private LevelConfigProvider _levelConfigProvider;
        [SerializeField] private ProjectileFactory _projectileFactory;
        [SerializeField] private TowerFactory _towerFactory;
        [SerializeField] private EnemyFactory _enemyFactory;
        [Space]
        [Header("Model")]
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private RaycastController _raycastController;
        [SerializeField] private TilemapController _tilemapController;
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

        private void Awake()
        {
            ResolveModelView();
        }

        private void ResolveModelView()
        {
            //Debug.Log(Container.ResolveAll<BehaviourCollection>().Count);
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
            BindFactories();
            BindScore();
            BindControllers();
            BindScenario();
            BindLevelCycle();
            BindLevelState();
        }

        private void BindLevelState()
        {
            Container.BindInterfacesAndSelfTo<LevelState>().AsSingle().NonLazy();
            Container.BindInstance(_config.PrepareTime).WhenInjectedInto<LevelState>();
        }

        private void BindInput()
        {
            Container.BindInterfacesAndSelfTo<ActionMapReader>().AsSingle().NonLazy();
        }

        private void BindFactories()
        {
            Container.BindInterfacesAndSelfTo<TowerFactory>().FromInstance(_towerFactory).AsSingle();
            Container.Bind<ProjectileFactory>().FromInstance(_projectileFactory).AsSingle();
            Container.Bind<EnemyFactory>().FromInstance(_enemyFactory).AsSingle();
        }

        private void BindScore()
        {
            Container.BindInterfacesAndSelfTo<Health>().AsSingle().WithArguments(_config.Health);
            Container.BindInterfacesAndSelfTo<Coins>().AsSingle().WithArguments(_config.Coins);
        }

        private void BindScenario()
        {
            Container.Bind<EnemyWavesConfig>().FromInstance(_enemyWavesConfig).AsSingle();
            Container.BindInterfacesAndSelfTo<Scenario>().AsSingle().NonLazy();
        }

        private void BindLevelCycle()
        {
            Container.Bind<LevelCycle>().FromInstance(_levelCycle).AsSingle();
        }

        private void BindControllers()
        {
            Container.BindInterfacesAndSelfTo<ProjectileController>().AsSingle();
            Container.BindInterfacesAndSelfTo<TowerController>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyContorller>().AsSingle().WithArguments(_pathPointsView.GetConfig());

            Container.BindInterfacesAndSelfTo<TilemapController>().FromInstance(_tilemapController).AsSingle();
            Container.BindInterfacesAndSelfTo<RaycastController>().FromInstance(_raycastController).AsSingle();
            Container.BindInterfacesAndSelfTo<ContentSelection>().AsSingle();
            Container.Bind<ILateUpdate>().FromInstance(_cameraController).AsSingle();
            Container.BindInterfacesAndSelfTo<TowerPreview>().AsSingle().NonLazy();
        }

        private void BindPresenters()
        {
            Container.Bind<ScorePresenter>().AsSingle().NonLazy();
            Container.Bind<LevelStateViewModel>().AsSingle().NonLazy();
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

        private void OnDisable()
        {
            Container.ResolveAll<IDispose>().ForEach(x => x.Dispose());
            Container.UnbindAll();
        }
    }
}