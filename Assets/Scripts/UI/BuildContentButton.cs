using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildContentButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private TileContentType _type;

    private Action<TileContentType> _listenerAction;

    public void AddListener(Action<TileContentType> listenerAction)
    {
        _listenerAction = listenerAction;
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        _listenerAction?.Invoke(_type);
    }
}
