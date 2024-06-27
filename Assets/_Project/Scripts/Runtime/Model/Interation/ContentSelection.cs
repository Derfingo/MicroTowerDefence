using System;
using UnityEngine;
using Zenject;

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

        public event Action<bool> SelectedEvent;
        public event Action<bool> CancelSelectedEvent;
        public event Action<uint, uint> ShowTowerMenuEvent;
        public event Action SelectToBuildEvent;

        private TileContent _previewContent;
        private TileContent _targetContent;
        private Vector3 _previewPosition;
        private IInputActions _input;
        private bool _isSelected;
        private bool _isGround;

        [Inject]
        public void Initialize(IInputActions input)
        {
            _input = input;

            _input.SelectPlaceEvent += OnSelectPlace;
            _input.CancelSelectPlaceEvent += OnCancelSelectedPlace;
            _towerController.TowerSelectedEvent += SelectTower;
            _raycastController.OnGround += (isGound) => _isGround = isGound;
        }

        private void OnSelectPlace()
        {
            if (_isSelected && _raycastController.GetContent() == null && _previewContent == null)
            {
                OnCancelSelectedPlace();
                return;
            }
            else if (_targetContent != null)
            {
                _targetContent.Undo();
                _isSelected = false;
            }


            _targetContent = _raycastController.GetContent();

            if (_targetContent == null && _isGround)
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
            SelectedEvent.Invoke(_isSelected);
            CancelSelectedEvent?.Invoke(false);
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
            SelectedEvent?.Invoke(_isSelected);
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

            OnCancelSelectedPlace();
        }

        public void OnUpgradeTower()
        {
            UpgradeEvent?.Invoke(_targetContent);
            OnCancelSelectedPlace();
        }

        public void OnSellTower()
        {
            var tower = _targetContent.GetComponent<TowerBase>();
            SellEvent?.Invoke(_targetContent, tower.SellCost);
            OnCancelSelectedPlace();
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

            _input.SetPlayerMap();
        }
    }
}