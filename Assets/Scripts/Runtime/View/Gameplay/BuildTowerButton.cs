using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildTowerButton : ViewBase, IPointerDownHandler
{
    [SerializeField] private TowerType _type;
    [SerializeField] private TextMeshProUGUI _cost;

    public TowerType Type => _type;

    private Action<TowerType> _listenerAction;

    public void SetCost(uint cost)
    {
        _cost.text = cost.ToString();
    }

    public void AddListener(Action<TowerType> listenerAction)
    {
        _listenerAction = listenerAction;
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        _listenerAction?.Invoke(_type);
    }
}
