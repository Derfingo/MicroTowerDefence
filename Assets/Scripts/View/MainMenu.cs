using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button _testLevelButton;

    private void Start()
    {
        _testLevelButton.onClick.AddListener(OnTestLevelClicked);
        SceneManager.UnloadSceneAsync(Constants.Scenes.Boot);
    }

    private void OnTestLevelClicked()
    {
        LoadTestLevel();
    }

    private async void LoadTestLevel()
    {
        var loadOp = SceneManager.LoadSceneAsync(Constants.Scenes.TEST_LEVEL, LoadSceneMode.Single);
        while (loadOp.isDone == false)
        {
            await Task.Delay(1);
        }
    }
}
