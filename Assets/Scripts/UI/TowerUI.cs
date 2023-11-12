using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private Button _upgradeTowerButton;
    [SerializeField] private Button _sellTowerButton;

    public void Show()
    {
        _menuPanel.SetActive(true);
    }

    public void Hide()
    {
        _menuPanel.SetActive(false);
    }
}
