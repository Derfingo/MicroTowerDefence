using UnityEngine;
using Zenject;

namespace MicroTowerDefence
{
    public class MainMenuLoader : MonoBehaviour
    {
        private ILoader _loader;

        [Inject]
        public void Initialize(ILoader loader)
        {
            _loader = loader;
        }

        private void Start()
        {
            _loader.LoadAsync(Constants.Scenes.MAIN_MENU, false);
        }
    }
}
