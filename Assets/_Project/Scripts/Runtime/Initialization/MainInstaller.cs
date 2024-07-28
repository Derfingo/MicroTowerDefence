using UnityEngine;
using Zenject;

namespace MicroTowerDefence
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private LevelLoader _levelLoader;

        public override void InstallBindings()
        {
            Container.Bind<LevelLoader>().FromInstance(_levelLoader).AsSingle().NonLazy();
        }
    }
}
