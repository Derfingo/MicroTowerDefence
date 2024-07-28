using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;

namespace MicroTowerDefence
{
    public class SimpleButton : ButtonBase
    {
        private readonly float _duration = 0.3f;

        public async override void Hide()
        {
            GetComponent<RectTransform>().DOScale(0f, _duration);
            await Task.Delay(300);
            base.Hide();
        }
    }
}