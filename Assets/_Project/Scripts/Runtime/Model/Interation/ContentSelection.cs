using System;
using UnityEngine;

namespace MicroTowerDefence
{
    public class ContentSelection : ISelection, IReset, IDisposable
    {
        public event Action<bool> OnBuildingEvent;
        public event Action<bool> OnInteractionEvent;
        public Vector3 GridPosition => _gridPosition;

        private readonly IInputActions _input;
        private readonly TowerPreview _towerPreview;
        private readonly TowerController _towerController;
        private readonly TilemapController _tilemapController;
        private readonly RaycastController _raycastController;

        private bool _isGround;
        private bool _isBuilding;
        private bool _isInteraction;
        private Vector3 _gridPosition;

        private TileContent _target;

        public ContentSelection(IInputActions input,
                                RaycastController raycastController,
                                TilemapController tilemapController,
                                TowerController teachController,
                                TowerPreview towerPreview)
       {
            _input = input;
            _towerPreview = towerPreview;
            _towerController = teachController;
            _raycastController = raycastController;
            _tilemapController = tilemapController;

            _input.OnSelectEvent += OnSelectToBuild;
            _tilemapController.OnGridEvent += GetGridState;
            _towerPreview.OnBuildEvent += (isBuilding) => SetBuildingState(false);
        }

        public void OnSellTower()
        {
            _towerController.OnSell(_target.GetComponent<TowerBase>());
            DeselectContent();
        }

        public void OnUpgradeTower()
        {
            _towerController.OnUpgrade(_target.GetComponent<TowerBase>());
            DeselectContent();
        }

        private void OnSelectToBuild()
        {
            if (_raycastController.GetContent(out TileContent content))
            {
                SetInteractionState(true);
                _target = content;
                _target.Interact();
            }
            else if (_isGround)
            {
                SetBuildingState(true);
            }
            else
            {
                DeselectContent();
            }
        }

        private void SetInteractionState(bool isInteraction)
        {
            _isInteraction = isInteraction;
            OnInteractionEvent?.Invoke(isInteraction);
        }

        private void SetBuildingState(bool isBuilding)
        {
            _isBuilding = isBuilding;
            OnBuildingEvent?.Invoke(isBuilding);
        }

        private void DeselectContent()
        {
            SetBuildingState(false);
            SetInteractionState(false);
            _target?.Undo();
            _target = null;
            _input.SetPlayerInput();
        }

        private void GetGridState(Vector3 position, bool isGround)
        {
            _isGround = isGround;
            if (_isBuilding || _isInteraction) return;
            _gridPosition = position;
        }

        public void Reset()
        {
            _target = null;
        }

        public void Dispose()
        {
            _input.OnSelectEvent -= OnSelectToBuild;
            _tilemapController.OnGridEvent -= GetGridState;
            _towerPreview.OnBuildEvent -= (isBuilding) => SetBuildingState(false);
        }
    }
}