using System;
using UnityEngine.EventSystems;

namespace MicroTowerDefence
{
    public abstract class ButtonBase : ViewBase, ISubmitHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        public event Action ClickEvent;
        public event Action PointerEnterEvent;
        public event Action PointerExitEvent;

        public void OnPointerDown(PointerEventData eventData)
        {
            ClickEvent?.Invoke();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            PointerEnterEvent?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            PointerExitEvent?.Invoke();
        }

        public void OnSubmit(BaseEventData eventData)
        {
            ClickEvent?.Invoke();
        }
    }
}