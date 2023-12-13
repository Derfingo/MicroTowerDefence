using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject _buttonsPanel;
    [SerializeField] private List<BuildTowerButton> _buttons;

    public List<BuildTowerButton> Buttons => _buttons;

    public void SetRotationPanel(Quaternion rotation)
    {
        _buttonsPanel.transform.localRotation = rotation;
    }

    public void Show()
    {
        _buttonsPanel.SetActive(true);
    }

    public void Hide()
    {
        _buttonsPanel.SetActive(false);
    }
}
