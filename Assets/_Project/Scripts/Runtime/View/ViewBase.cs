using UnityEngine;

namespace MicroTowerDefence
{
    public abstract class ViewBase : MonoBehaviour
    {
        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }

        public virtual void Show()
        {
            gameObject.SetActive(true);
        }
    }
}