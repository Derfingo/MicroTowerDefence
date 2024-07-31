using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MicroTowerDefence
{
    public class ButtonView : Selectable, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public event Action OnClickEvent;
        public event Action OnPointerEnterEvent;
        public event Action OnPointerExitEvent;

        public void Initialize()
        {
            image = GetComponent<Image>();
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            OnClickEvent?.Invoke();
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            OnPointerEnterEvent?.Invoke();
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            OnPointerExitEvent?.Invoke();
        }
    }
}