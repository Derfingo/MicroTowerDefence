using UnityEngine;
using UnityEngine.SceneManagement;

namespace MicroTowerDefence
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private LevelsView _levelsView;

        private void Start()
        {
            SceneManager.UnloadSceneAsync(Constants.Scenes.Bootstrap);
            print("Bootstrap is unloaded");

            for (int i = 0; i < _levelsView.LevelButtons.Length; i++)
            {
                _levelsView.LevelButtons[i].Initialize((i + 1).ToString());
                _levelsView.LevelButtons[i].OnClickEvent += LoadLevel;
            }
        }

        private void LoadLevel(string nameLevel)
        {
            SceneManager.LoadScene(nameLevel, LoadSceneMode.Single);
            Debug.Log("Level loading");
        }
    }
}