using System;
using TMPro;
using UnityEngine;

namespace MicroTowerDefence
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextView : ViewBase
    {
        [NonSerialized] public TextMeshProUGUI Text;

        public void Initialize(IReactiveProperty<string> property)
        {
            Text = GetComponent<TextMeshProUGUI>();
            property.OnChangeEvent += OnChangeText;
        }

        private void OnChangeText(string text)
        {
            Text.text = text;
        }
    }
}