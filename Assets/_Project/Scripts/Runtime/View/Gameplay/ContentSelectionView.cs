using System;
using UnityEngine;
using Zenject;

namespace MicroTowerDefence
{
    public class ContentSelectionView : ViewBase, ISelectionView, IUpdate
    {
        [SerializeField] private GameplayButtonsView _gameplayButtonsView;
        [SerializeField] private TargetCellView _targetCellView;

        public event Action<TowerType> BuildClickEvent;
        public event Action UpgradeClickEvent;
        public event Action SellClickEvent;
        public event Action<TowerType> ShowPreviewEvent;
        public event Action<TowerType> HidePreviewEvent;

        public event Func<Vector3> CellCenterPositionEvent;

        private IRaycast _raycast;
        private bool _isEnableCursor;

        [Inject]
        public void Initialize(IRaycast raycast)
        {
            _raycast = raycast;

            _gameplayButtonsView.BuildTowerEvent += OnBuildTower;
            _gameplayButtonsView.SellTowerEvent += OnSellSelectedTower;
            _gameplayButtonsView.UpgradeTowerEvent += OnUpgradeTower;

            _gameplayButtonsView.EnterPreviewTowerEvent += OnShowPreview;
            _gameplayButtonsView.ExitPreviewTowerEvent += OnHidePreview;

            _gameplayButtonsView.PointerEnterEvent += OnPointerOverButton;
            _gameplayButtonsView.PointerExitEvent += OnPointerOutButton;

            _targetCellView.Show();
        }

        public void GameUpdate()
        {
            UpdateTargetCellView();
        }

        public void IsEnableCursor(bool isEnable)
        {
            _isEnableCursor = isEnable;
        }

        public void OnHideButtons()
        {
            _gameplayButtonsView.HideButtonViews();
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
            _raycast.SetOverUI(true);
        }

        private void OnPointerOutButton()
        {
            _raycast.SetOverUI(false);
        }

        private void UpdateTargetCellView()
        {
            var position = CellCenterPositionEvent.Invoke();
            _targetCellView.UpdatePosition(position);
            _targetCellView.Display(_isEnableCursor);
        }
    }
}