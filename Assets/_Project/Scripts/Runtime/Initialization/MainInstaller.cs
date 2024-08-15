using UnityEngine;
using Zenject;

namespace MicroTowerDefence
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private LevelLoader _levelLoader;

        [Space]
        [Header("View")]
        [SerializeField] private Transform _rootView;

        public override void InstallBindings()
        {
            BindLoader();
            BindProviders();
            BindMenuTransition();
        }

        private void BindMenuTransition()
        {
            Container.Bind<Transform>().FromInstance(_rootView).AsSingle().NonLazy();
            Container.Bind<MainMenuTransition>().AsSingle().NonLazy();
        }

        private void BindLoader()
        {
            Container.Bind<LevelLoader>().FromInstance(_levelLoader).AsSingle().NonLazy();
        }

        private void BindProviders()
        {
            Container.Bind<MainViewProvider>().AsSingle().NonLazy();
            Container.Bind<LevelsViewProvider>().AsSingle().NonLazy();
            Container.Bind<SettingsViewProvider>().AsSingle().NonLazy();
        }
    }
}
