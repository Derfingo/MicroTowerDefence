using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MicroTowerDefence
{
    public class LevelButton : ViewBase, IPointerDownHandler
    {
        public event Action<string> OnClickEvent;
        public Image Image => _image;

        private string _nameLevel;
        private Image _image;

        public void Initialize(string nameLevel)
        {
            _nameLevel = nameLevel;
            _image = GetComponent<Image>();
        }

        public override string ToString()
        {
            return _nameLevel;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnClickEvent?.Invoke(_nameLevel);
        }
    }
}