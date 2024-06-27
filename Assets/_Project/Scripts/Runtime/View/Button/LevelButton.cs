using System;
using UnityEngine.EventSystems;

namespace MicroTowerDefence
{
    public class LevelButton : ViewBase, IPointerDownHandler
    {
        public event Action<string> OnClickEvent;

        private string _nameLevel;

        public void Initialize(string nameLevel)
        {
            _nameLevel = nameLevel;
        }

        public override string ToString()
        {
            return _nameLevel;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnClickEvent?.Invoke(_nameLevel);
            print("click");
        }
    }
}