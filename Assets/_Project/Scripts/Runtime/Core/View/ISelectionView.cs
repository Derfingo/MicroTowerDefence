using System;
using UnityEngine;

namespace MicroTowerDefence
{
    public interface ISelectionView
    {
        public event Action<TowerType> BuildClickEvent;
        public event Action UpgradeClickEvent;
        public event Action SellClickEvent;

        public event Action<TowerType> ShowPreviewEvent;
        public event Action<TowerType> HidePreviewEvent;
        public event Func<Vector3> CellCenterPositionEvent;

        void OnHideButtons();
        void ShowTowerMenu(uint upgradeCost, uint sellCost);
        void ShowMenuToBuild();

        void IsEnableCursor(bool isEnable);
    }
}