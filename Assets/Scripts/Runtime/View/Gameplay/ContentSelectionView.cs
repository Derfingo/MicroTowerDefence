using System;
using UnityEngine;

namespace MicroTowerDefence
{
    public class ContentSelectionView : ViewBase, ISelectionView, IUpdate
    {
        [SerializeField] private GameplayButtonsView _gameplayButtonsView;
        [SerializeField] private TargetCellView _targetCellView;
        [SerializeField] private TowerPlaceView[] _towerPlaces;

        public event Action<TowerType> BuildClickEvent;
        public event Action UpgradeClickEvent;
        public event Action SellClickEvent;
        public event Action<TowerType> ShowPreviewEvent;
        public event Action<TowerType> HidePreviewEvent;

        public event Func<Vector3> CellCenterPositionEvent;

        private IInputActions _input;
        private bool _isEnableCursor;
        private bool _isSelected;

        public void Initialize(IInputActions input)
        {
            _input = input;

            _input.TowerPlacesEvent += OnDisplayTowerPlaces;

            _gameplayButtonsView.BuildTowerEvent += OnBuildTower;
            _gameplayButtonsView.SellTowerEvent += OnSellSelectedTower;
            _gameplayButtonsView.UpgradeTowerEvent += OnUpgradeTower;

            _gameplayButtonsView.EnterPreviewTowerEvent += OnShowPreview;
            _gameplayButtonsView.ExitPreviewTowerEvent += OnHidePreview;

            _gameplayButtonsView.PointerEnterEvent += OnPointerOverButton;
            _gameplayButtonsView.PointerExitEvent += OnPointerOutButton;

            _targetCellView.Show();
            OnDisplayTowerPlaces(false);
        }

        public void GameUpdate()
        {
            UpdateTargetCellView();
        }

        public void IsEnableCursor(bool isEnable)
        {
            _isEnableCursor = isEnable;
        }

        public void OnSelectedContent(bool isSelected)
        {
            _isSelected = isSelected;
        }

        private void OnDisplayTowerPlaces(bool isEnable)
        {
            foreach (var place in _towerPlaces)
            {
                place.Display(isEnable);
            }
        }

        public void OnCancelSelected(bool isSelected)
        {
            _gameplayButtonsView.HideButtonViews();
            _isSelected = isSelected;
        }

        public void ShowMenuToBuild()
        {
            _gameplayButtonsView.ShowBuildTowerView();
        }

        public void ShowTowerMenu(uint upgradeCost, uint sellCost)
        {
            _gameplayButtonsView.ShowTowerView(upgradeCost, sellCost);
        }

        private void OnBuildTower(TowerType type)
        {
            BuildClickEvent?.Invoke(type);
            _gameplayButtonsView.HideButtonViews();
        }

        private void OnShowPreview(TowerType type)
        {
            ShowPreviewEvent?.Invoke(type);
        }

        private void OnHidePreview(TowerType type)
        {
            HidePreviewEvent?.Invoke(type);
        }

        private void OnUpgradeTower()
        {
            UpgradeClickEvent?.Invoke();
            _gameplayButtonsView.HideButtonViews();
        }

        private void OnSellSelectedTower()
        {
            SellClickEvent?.Invoke();
            _gameplayButtonsView.HideButtonViews();
        }

        private void OnPointerOverButton()
        {
            _input.SetUIMap();
        }

        private void OnPointerOutButton()
        {
            _input.SetPlayerMap();
        }

        private void UpdateTargetCellView()
        {
            if (_isSelected)
            {
                return;
            }

            var position = CellCenterPositionEvent.Invoke();     // func
            _targetCellView.UpdatePosition(position);
            _targetCellView.Display(_isEnableCursor);   // func
        }
    }
}