using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject _buttonsPanel;
    [SerializeField] private List<BuildButton> _buttons;

    public List<BuildButton> Buttons => _buttons;

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
