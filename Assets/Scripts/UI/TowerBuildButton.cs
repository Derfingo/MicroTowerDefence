using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerBuildButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private TowerType _type;

    private Action<TowerType> _listenerAction;

    public void AddListener(Action<TowerType> listenerAction)
    {
        _listenerAction = listenerAction;
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        _listenerAction?.Invoke(_type);
    }
}
