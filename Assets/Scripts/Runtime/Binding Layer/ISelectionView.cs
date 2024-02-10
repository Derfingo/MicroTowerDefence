using System;
using UnityEngine;

public interface ISelectionView
{
    public event Action<TowerType> BuildClickEvent;
    public event Action UpgradeClickEvent;
    public event Action SellClickEvent;
    public event Action<TowerType> ShowPreviewEvent;
    public event Action<TowerType> HidePreviewEvent;

    public event Func<Vector3> CellCenterPositionEvent;
    public event Func<bool> RaycastHitEvent;

    void OnSelectedContent(bool isSelected);
    void ShowTowerMenu(uint upgradeCost, uint sellCost);
    void ShowMenuToBuild();
    void OnCancelSelected();
}
