using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace MicroTowerDefence
{
    public class GameLoadingOperation : ILoadingOperation
    {
        public string GetName => "Game loading";

        public async Task Load(Action<float> onProgress)
        {
            onProgress?.Invoke(0.5f);
            var loadOp = SceneManager.LoadSceneAsync(Constants.Scenes.TEST_LEVEL, LoadSceneMode.Single);

            while (loadOp.isDone == false)
            {
                await Task.Delay(1);
            }
            onProgress?.Invoke(1f);
        }
    }
}