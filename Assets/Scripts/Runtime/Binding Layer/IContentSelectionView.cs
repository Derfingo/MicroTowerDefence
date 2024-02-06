using System;
using UnityEngine;

public interface IContentSelectionView
{
    public event Predicate<uint> CheckCoinsEvent;

    public event Func<Vector3Int> WorldGridPositionEvent;
    public event Func<Vector3> CellCenterPositionEvent;
    public event Func<float> HeightTilemapEvent;

    public event Func<TileContent> GetContentEvent;
    public event Func<bool> RaycastHitEvent;
}
