using UnityEngine;

namespace MicroTowerDefence
{
    public class InteractionPresenter : MonoBehaviour
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

            _raycast.OnGround += _selectionView.IsEnableCursor;

            _selectionView.CellCenterPositionEvent += _grid.GetCellCenterPosition;

            _selectionView.UpgradeClickEvent += _selection.OnUpgradeTower;
            _selectionView.BuildClickEvent += _selection.OnBuildTower;
            _selectionView.SellClickEvent += _selection.OnSellTower;
            _selectionView.ShowPreviewEvent += _selection.OnShowPreview;
            _selectionView.HidePreviewEvent += _selection.OnHidePreview;

            _selection.SelectedEvent += _selectionView.OnSelectedContent;
            _selection.CancelSelectedEvent += _selectionView.OnCancelSelected;
            _selection.ShowTowerMenuEvent += _selectionView.ShowTowerMenu;
            _selection.SelectToBuildEvent += _selectionView.ShowMenuToBuild;
        }
    }
}