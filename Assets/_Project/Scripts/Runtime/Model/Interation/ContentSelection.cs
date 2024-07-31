using System;
using UnityEngine;

namespace MicroTowerDefence
{
    public class ContentSelection : ISelection
    {
        public event Action<TileContent, Vector3> BuildEvent;
        public event Action<TileContent, uint> SellEvent;
        public event Action<TileContent> UpgradeEvent;

        public event Action OnCancelSelectedEvent;
        public event Action SelectToBuildEvent;

        private readonly TilemapController _tilemapController;
        private readonly RaycastController _raycastController;
        private readonly TowerFactory _towerFactory;
        private readonly IInputActions _input;
        private readonly Coins _coins;

        private TileContent _previewContent;
        private TileContent _targetContent;
        private Vector3 _previewPosition;
        private bool _isGround = false;

       public ContentSelection(IInputActions input,
                               RaycastController raycastController,
                               TilemapController tilemapController,
                               TowerFactory factory,
                               Coins coins)
       {
            _input = input;
            _raycastController = raycastController;
            _tilemapController = tilemapController;
            _towerFactory = factory;
            _coins = coins;

            _input.OnSelectEvent += OnSelect;
            _raycastController.OnGround += (isGound) => _isGround = isGound;
       }

        private void OnSelect()
        {
            if (_raycastController.CheckOverUI())
            {
                return;
            }
            else
            {
                if (_raycastController.GetContent(out TileContent content))
                {
                    _targetContent = content;
                    content.Interact();
                }
                else if (_isGround)
                {
                    SelectToBuild();
                }

                CancelSelected();
            }
        }

        private void CancelSelected()
        {
            _targetContent?.Undo();
            _targetContent = null;
            //OnCancelSelectedEvent?.Invoke();
        }

        private void SelectToBuild()
        {
            _previewPosition = _tilemapController.GetCellCenterPosition();
            SelectToBuildEvent?.Invoke();
        }

        public void OnBuildTower(TowerType type)
        {
            if (_previewContent != null)
            {
                BuildEvent?.Invoke(_previewContent, _previewPosition);
            }
            else
            {
                Debug.Log("preview is null");
            }

            CancelSelected();
        }

        public void OnUpgradeTower()
        {
            UpgradeEvent?.Invoke(_targetContent);
            CancelSelected();
        }

        public void OnSellTower()
        {
            var tower = _targetContent.GetComponent<TowerBase>();
            SellEvent?.Invoke(_targetContent, tower.SellCost);
            CancelSelected();
        }

        public void OnShowPreview(TowerType type)
        {
            _previewContent = _towerFactory.Get(type);
            TowerBase tower = _previewContent.GetComponent<TowerBase>();
            var cost = _towerFactory.GetCostToBuild(tower.TowerType);

            if (_coins.Check(cost))
            {
                _previewContent.Position = _previewPosition;
                _previewContent.Show();
                tower.ShowTargetRadius(true);
                tower.Show();
            }
            else
            {
                _previewContent.Reclaim();
                // show not enough coins
            }
        }

        public void OnHidePreview(TowerType type)
        {
            if (_previewContent != null)
            {
                _previewContent.Reclaim();
                _previewContent = null;
            }
        }

        ~ContentSelection()
        {
            _input.OnSelectEvent -= OnSelect;
            _raycastController.OnGround -= (isGound) => _isGround = isGound;
        }
    }
}