using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace MicroTowerDefence
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private LevelsView _levelsView;

        private SceneLoader _sceneLoader;

        [Inject]
        public void Initialize(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private void Start()
        {
            if (SceneManager.GetSceneByName(Constants.Scenes.BOOTSTRAP).isLoaded)
            {
                SceneManager.UnloadSceneAsync(Constants.Scenes.BOOTSTRAP);
            }

            for (int i = 0; i < _levelsView.LevelButtons.Length; i++)
            {
                _levelsView.LevelButtons[i].Initialize((i + 1).ToString());
                _levelsView.LevelButtons[i].OnClickEvent += (name) => _sceneLoader.LoadAsync(name);
            }
        }
    }
}