using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using Zenject;

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

        public async void LoadAsync(string name, bool isFadeIn = true, Action onComplete = null)
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

            await SceneManager.LoadSceneAsync(name);
            await FadeAsync(FADE_OUT, _fadeVelocity);
            onComplete?.Invoke();
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
