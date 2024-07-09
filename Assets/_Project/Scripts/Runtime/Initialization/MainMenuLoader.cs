using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace MicroTowerDefence
{
    public class MainMenuLoader : MonoBehaviour
    {
        [SerializeField] private TMP_Text _label;

        private AsyncOperation _loadingOperation;
        
        public void Start()
        {
            Load();
            AppearLabel();
        }

        private void Load()
        {
            _loadingOperation = SceneManager.LoadSceneAsync(Constants.Scenes.MAIN_MENU, LoadSceneMode.Additive);
            _loadingOperation.allowSceneActivation = false;
        }

        private void AppearLabel()
        {
            _label.rectTransform.DOLocalMoveY(0f, 0.8f)
                .SetEase(Ease.InOutQuart)
                .SetLink(gameObject)
                .OnKill(ActivateSceneAsync);
        }

        private async void ActivateSceneAsync()
        {
            _loadingOperation.allowSceneActivation = true;
            await _loadingOperation;
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(Constants.Scenes.MAIN_MENU));
        }
    }
}
