using UnityEngine;

namespace MicroTowerDefence
{
    public class SelectionPresenter : MonoBehaviour
    {
        private IGrid _grid;
        private IRaycast _raycast;
        private ISelection _selection;
        private ISelectionView _selectionView;

        public void Initialize(ISelection selection, IGrid grid, IRaycast raycast, ISelectionView selectionView)
        {
            _grid = grid;
            _raycast = raycast;
            _selection = selection;
            _selectionView = selectionView;

            _selectionView.RaycastHitEvent += _raycast.CheckHit;

            _selectionView.CellCenterPositionEvent += _grid.GetCellCenterPosition;

            _selectionView.UpgradeClickEvent += _selection.OnUpgradeTower;
            _selectionView.BuildClickEvent += _selection.OnBuildTower;
            _selectionView.SellClickEvent += _selection.OnSellTower;
            _selectionView.ShowPreviewEvent += _selection.OnShowPreview;
            _selectionView.HidePreviewEvent += _selection.OnHidePreview;

            _selection.SelectedContentEvent += _selectionView.OnSelectedContent;
            _selection.ShowTowerMenuEvent += _selectionView.ShowTowerMenu;
            _selection.SelectToBuildEvent += _selectionView.ShowMenuToBuild;
            _selection.CancelSelectedEvent += _selectionView.OnCancelSelected;
        }
    }
}