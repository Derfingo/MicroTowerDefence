using System;
using UnityEngine;

namespace MicroTowerDefence
{
    public class ContentSelection : MonoBehaviour, ISelection
    {
        [SerializeField] private TilemapController _tilemapController;
        [SerializeField] private RaycastController _raycastController;
        [SerializeField] private TowerController _towerController;
        [SerializeField] private TowerFactory _towerFactory;
        [SerializeField] private Coins _coins;

        public event Action<TileContent, Vector3> BuildEvent;
        public event Action<TileContent, uint> SellEvent;
        public event Action<TileContent> UpgradeEvent;

        public event Action<bool> SelectedContentEvent;
        public event Action<uint, uint> ShowTowerMenuEvent;
        public event Action SelectToBuildEvent;
        public event Action CancelSelectedEvent;

        private TileContent _previewContent;
        private TileContent _targetContent;
        private Vector3 _previewPosition;
        private IInputActions _input;
        private bool _isSelected;

        public void Initialize(IInputActions input)
        {
            _input = input;

            _input.SelectPlaceEvent += OnSelectPlace;
            _input.CancelSelectPlaceEvent += OnCancelSelectedPlace;
            _towerController.TowerSelectedEvent += SelectTower;
        }

        private void OnSelectPlace()
        {
            if (_isSelected && _raycastController.GetContent() == null && _previewContent == null)
            {
                OnCancelSelectedPlace();
                return;
            }

            _targetContent = _raycastController.GetContent();

            if (_targetContent == null && _raycastController.CheckHit())
            {
                SelectToBuild();
            }
            else if (_targetContent != null)
            {
                _targetContent.Interact();
            }
        }

        private void OnCancelSelectedPlace()
        {
            _targetContent?.Undo();
            _targetContent = null;
            _isSelected = false;
            SelectedContentEvent.Invoke(_isSelected);
            CancelSelectedEvent?.Invoke();
            _input.SetPlayerMap();
        }

        private void SelectToBuild()
        {
            _isSelected = true;
            _previewPosition = _tilemapController.GetCellCenterPosition();
            SelectToBuildEvent?.Invoke();
        }

        public void SelectTower(TileContent content)
        {
            _isSelected = true;
            _targetContent = content;
            var tower = content.GetComponent<TowerBase>();
            tower.Show();
            ShowTowerMenuEvent?.Invoke(tower.UpgradeCost, tower.SellCost);
            SelectedContentEvent?.Invoke(_isSelected);
        }

        public void OnBuildTower(TowerType type)
        {
            if (_previewContent != null)
            {
                BuildEvent?.Invoke(_previewContent, _previewPosition);
                _previewContent = null;
            }
            else
            {
                Debug.Log("preview is null");
            }

            _input.SetPlayerMap();
            _isSelected = false;
            SelectedContentEvent.Invoke(_isSelected);
        }

        public void OnUpgradeTower()
        {
            UpgradeEvent?.Invoke(_targetContent);
            _targetContent = null;
            _isSelected = false;
            SelectedContentEvent.Invoke(_isSelected);
            _input.SetPlayerMap();
        }

        public void OnSellTower()
        {
            var tower = _targetContent.GetComponent<TowerBase>();
            SellEvent?.Invoke(_targetContent, tower.SellCost);
            _targetContent = null;
            _isSelected = false;
            SelectedContentEvent.Invoke(_isSelected);
            _input.SetPlayerMap();
        }

        public void OnShowPreview(TowerType type)
        {
            _input.SetUIMap();
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
                _previewContent.Destroy();
                // show not enough coins
            }
        }

        public void OnHidePreview(TowerType type)
        {
            if (_previewContent != null)
            {
                _previewContent.Destroy();
                _previewContent = null;
            }

            _input.SetPlayerMap();
        }
    }
}