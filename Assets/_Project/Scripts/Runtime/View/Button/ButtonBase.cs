using System;
using UnityEngine.EventSystems;

namespace MicroTowerDefence
{
    public abstract class ButtonBase : ViewBase, ISubmitHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        public event Action OnClickEvent;
        public event Action OnPointerEnterEvent;
        public event Action OnPointerExitEvent;

        public void OnPointerDown(PointerEventData eventData)
        {
            OnClickEvent?.Invoke();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnPointerEnterEvent?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnPointerExitEvent?.Invoke();
        }

        public void OnSubmit(BaseEventData eventData)
        {
            OnClickEvent?.Invoke();
        }
    }
}