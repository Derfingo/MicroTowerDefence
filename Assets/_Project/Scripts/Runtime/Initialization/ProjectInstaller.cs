using UnityEngine;
using Zenject;

namespace MicroTowerDefence
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private SceneLoader _sceneLoaderPrefab;

        // create prefab with inject
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SceneLoader>().
                      FromComponentInNewPrefab(_sceneLoaderPrefab).
                      AsSingle();
        }
    }
}
