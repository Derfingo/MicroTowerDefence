using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MicroTowerDefence
{
    [RequireComponent(typeof(Image))]
    public class LevelButton : Selectable, IPointerDownHandler
    {
        public event Action<string> OnClickEvent;

        private TextMeshProUGUI _numberLevel;

        public void Initialize(int level)
        {
            image = GetComponent<Image>();
            _numberLevel = GetComponentInChildren<TextMeshProUGUI>();
            _numberLevel.text = level.ToString();
            gameObject.name = $"Level Button {_numberLevel.text}";
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            OnClickEvent?.Invoke(_numberLevel.text);
        }
    }
}