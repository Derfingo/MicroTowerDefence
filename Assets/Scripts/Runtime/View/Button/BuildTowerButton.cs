using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MicroTowerDefence
{
    [RequireComponent(typeof(Image))]
    public class BuildTowerButton : ViewBase, ISubmitHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        [SerializeField] private TowerType _type;

        public Action<TowerType> ClickEvent;
        public Action<TowerType> PointerEnterEvent;
        public Action<TowerType> PointerExitEvent;
        public TowerType Type => _type;

        private TMP_Text _costText;

        private void OnEnable()
        {
            _costText = GetComponentInChildren<TMP_Text>();
        }

        public void SetCost(uint cost)
        {
            _costText.text = cost.ToString();
        }

        public void OnSubmit(BaseEventData eventData)
        {
            ClickEvent?.Invoke(_type);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            PointerEnterEvent?.Invoke(_type);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            PointerExitEvent?.Invoke(_type);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            ClickEvent?.Invoke(_type);
        }
    }
}