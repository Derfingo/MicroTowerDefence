using System;
using UnityEngine;

namespace MicroTowerDefence
{
    public class ContentSelection : ISelection
    {
        public event Action<bool> OnBuildingEvent;
        public Vector3 GridPosition => _gridPosition;

        private readonly IInputActions _input;
        private readonly TowerPreview _towerPreview;
        private readonly TilemapController _tilemapController;
        private readonly RaycastController _raycastController;

        private bool _isGround;
        private bool _isBuilding;
        private Vector3 _gridPosition;

        public ContentSelection(IInputActions input,
                                RaycastController raycastController,
                                TilemapController tilemapController,
                                TowerPreview towerPreview)
       {
            _input = input;
            _towerPreview = towerPreview;
            _raycastController = raycastController;
            _tilemapController = tilemapController;

            _input.OnSelectEvent += OnSelectToBuild;
            _tilemapController.OnGridEvent += GetGridState;
            _towerPreview.OnBuildEvent += (isBuilding) => SetBuildingState(false);
        }

        private void OnSelectToBuild()
        {
            if (_isGround && _raycastController.GetContent(out TileContent content) == false)
            {
                //Debug.Log($"select to build: Position {_gridPosition}, Ground {_isGround}");
                SetBuildingState(true);
            }
            else
            {
                SetBuildingState(false);
            }
        }

        private void SetBuildingState(bool isBuilding)
        {
            _isBuilding = isBuilding;
            OnBuildingEvent?.Invoke(isBuilding);
        }

        private void GetGridState(Vector3 position, bool isGround)
        {
            _isGround = isGround;
            if (_isBuilding) return;
            _gridPosition = position;
        }

        ~ContentSelection()
        {
            _input.OnSelectEvent -= OnSelectToBuild;
            _tilemapController.OnGridEvent -= GetGridState;
            _towerPreview.OnBuildEvent -= (isBuilding) => SetBuildingState(false);
        }
    }
}