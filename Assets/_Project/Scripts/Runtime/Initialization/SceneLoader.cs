using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace MicroTowerDefence
{
    public class SceneLoader : MonoBehaviour, ILoader
    {
        [SerializeField, Range(0.5f, 2.5f)] private float _fadeVelocity;
        [SerializeField] private CanvasGroup _canvasGroup;

        private const int MILLISECOND = 1;
        private const float FADE_OUT = 0f;
        private const float FADE_IN = 1f;
        private float _currentAlpha;

        [Inject]
        public void Initialize()
        {
            _canvasGroup.alpha = 0f;
        }

        public async Task LoadAddressableAsync(string name, bool isFadeIn = true)
        {
            if (isFadeIn)
            {
                await FadeAsync(FADE_IN, _fadeVelocity);
            }
            else
            {
                _currentAlpha = FADE_IN;
                _canvasGroup.alpha = FADE_IN;
            }

            await Addressables.LoadSceneAsync(name).Task;
            await FadeAsync(FADE_OUT, _fadeVelocity);
        }

        public async Task LoadAsync(string name, bool isFadeIn = true)
        {
            if (isFadeIn)
            {
                await FadeAsync(FADE_IN, _fadeVelocity);
            }
            else
            {
                _currentAlpha = FADE_IN;
                _canvasGroup.alpha = FADE_IN;
            }

            await SceneManager.LoadSceneAsync(name).ToUniTask();
            await FadeAsync(FADE_OUT, _fadeVelocity);
        }

        public async Task LoadNextLevel()
        {
            var sceneNumber = int.Parse(SceneManager.GetActiveScene().name);
            var nextSceneName = (sceneNumber + 1).ToString();
            await LoadAddressableAsync(nextSceneName);
        }

        private async Task<Task> FadeAsync(float endValue, float fadeVelocity)
        {
            while (!Mathf.Approximately(_currentAlpha, endValue))
            {
                _currentAlpha = Mathf.MoveTowards(_currentAlpha, endValue, fadeVelocity * Time.deltaTime);
                _canvasGroup.alpha = _currentAlpha;
                await Task.Delay(MILLISECOND);
            }

            return Task.CompletedTask;
        }
    }
}
