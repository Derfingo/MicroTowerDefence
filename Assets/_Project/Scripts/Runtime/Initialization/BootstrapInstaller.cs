using UnityEngine;
using Zenject;

namespace MicroTowerDefence
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private MainMenuLoader _mainMenuLoader;

        public override void InstallBindings()
        {
            Container.Bind<MainMenuLoader>().FromInstance(_mainMenuLoader).AsSingle().NonLazy();
        }
    }
}
