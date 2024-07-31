using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace MicroTowerDefence
{
    public class LevelLoader : MonoBehaviour
    {
        private LevelsView _levelsView;
        private SceneLoader _sceneLoader;

        [Inject]
        public void Initialize(SceneLoader sceneLoader, LevelsView levelsView)
        {
            _sceneLoader = sceneLoader;
            _levelsView = levelsView;

            if (SceneManager.GetSceneByName(Constants.Scenes.BOOTSTRAP).isLoaded)
            {
                SceneManager.UnloadSceneAsync(Constants.Scenes.BOOTSTRAP);
                Debug.Log("loaded");
            }

            if (_levelsView.LevelButtons.Count == 0)
            {
                Debug.Log(0);
            }

            for (int i = 0; i < _levelsView.LevelButtons.Count; i++)
            {
                //_levelsView.LevelButtons[i].Initialize((i + 1).ToString());
                _levelsView.LevelButtons[i].OnClickEvent += (name) => _sceneLoader.LoadAsync(name, true);
            }
        }
    }
}