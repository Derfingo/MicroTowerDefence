using UnityEngine;
using Zenject;

namespace MicroTowerDefence
{
    public class LevelLoader : MonoBehaviour
    {
        private LevelsView _levelsView;
        private SceneLoader _sceneLoader;
        private MainMenuTransition _mainMenuTransition;

        [Inject]
        public void Initialize(SceneLoader sceneLoader, MainMenuTransition mainMenuTransition)
        {
            _sceneLoader = sceneLoader;
            _mainMenuTransition = mainMenuTransition;
            _mainMenuTransition.OnLevelsViewEvent += OnLevelsViewLoaded;

            //if (SceneManager.GetSceneByName(Constants.Scenes.BOOTSTRAP).isLoaded)
            //{
            //    SceneManager.UnloadSceneAsync(Constants.Scenes.BOOTSTRAP);
            //    Debug.Log("bootstrap is unloaded");
            //}
        }

        private void OnLevelsViewLoaded(LevelsView levelsView)
        {
            _levelsView = null;
            _levelsView = levelsView;

            if (_levelsView.LevelButtons == null || _levelsView.LevelButtons.Count == 0)
            {
                Debug.Log("No level buttons");
                return;
            }

            for (int i = 0; i < levelsView.LevelButtons.Count; i++)
            {
                _levelsView.LevelButtons[i].OnClickEvent += async (name) => await _sceneLoader.LoadAddressableAsync(name);
            }
        }

        private void OnDestroy()
        {
            _mainMenuTransition.OnLevelsViewEvent -= OnLevelsViewLoaded;
        }
    }
}