using UnityEngine;
using UnityEngine.SceneManagement;

namespace MicroTowerDefence
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private LevelsView _levelsView;

        private void Start()
        {
            SceneManager.UnloadSceneAsync(Constants.Scenes.Boot);

            foreach (var level in _levelsView.LevelButtons)
            {
                level.onClick.AddListener(LoadLevel);
            }
        }

        private void LoadLevel()
        {
            SceneManager.LoadScene(Constants.Scenes.TEST_LEVEL, LoadSceneMode.Single);
            Debug.Log("Level loading");
        }
    }
}