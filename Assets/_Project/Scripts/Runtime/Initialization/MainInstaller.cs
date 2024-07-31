using UnityEngine;
using Zenject;

namespace MicroTowerDefence
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private MainMenuTransition _mainMenuTransition;
        [SerializeField] private LevelLoader _levelLoader;
        [SerializeField] private LevelsView _levelsView;

        [Space]
        [Header("View")]
        [SerializeField] private MainView _mainView;

        public override void InstallBindings()
        {
            BindLoader();
            BindView();
        }

        private void BindLoader()
        {
            Container.Bind<LevelLoader>().FromInstance(_levelLoader).AsSingle().NonLazy();
        }

        private void BindView()
        {
            Container.Bind<MainView>().FromInstance(_mainView).AsSingle().NonLazy();
            Container.Bind<MainMenuTransition>().FromInstance(_mainMenuTransition).AsSingle().NonLazy();
            Container.Bind<LevelsView>().FromInstance(_levelsView).AsSingle().NonLazy();

        }
    }
}
