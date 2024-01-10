using TMPro;
using UnityEngine;

public class HealthView : ViewBase
{
    [SerializeField] private TextMeshProUGUI _healthText;

    public void SetHealth(uint health)
    {
        _healthText.text = health.ToString();
    }
}
