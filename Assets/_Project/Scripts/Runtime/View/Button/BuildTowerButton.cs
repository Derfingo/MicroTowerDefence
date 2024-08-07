using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MicroTowerDefence
{
    [RequireComponent(typeof(Image))]
    public class BuildTowerButton : Selectable, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private TowerType _type;

        public Action<TowerType> OnClickEvent;
        public Action<TowerType> OnPointerEnterEvent;
        public Action<TowerType> OnPointerExitEvent;
        public TowerType TowerType => _type;

        private TMP_Text _costText;

        public void Initialize()
        {
            image = GetComponent<Image>();
            _costText = GetComponentInChildren<TMP_Text>();
        }

        public void SetCost(uint cost)
        {
            _costText.text = cost.ToString();
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            OnClickEvent?.Invoke(_type);
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            OnPointerEnterEvent?.Invoke(_type);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            OnPointerExitEvent?.Invoke(_type);
        }
    }
}