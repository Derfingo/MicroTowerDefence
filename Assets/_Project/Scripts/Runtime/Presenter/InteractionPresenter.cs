namespace MicroTowerDefence
{
    public class InteractionPresenter
    {
        private readonly IGrid _grid;
        private readonly IRaycast _raycast;
        private readonly ISelection _selection;
        private readonly ISelectionView _selectionView;

        public InteractionPresenter(IGrid grid, IRaycast raycast, ISelection selection, ISelectionView selectionView)
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

        ~InteractionPresenter()
        {
            _raycast.OnGround -= _selectionView.IsEnableCursor;
            _selectionView.CellCenterPositionEvent -= _grid.GetCellCenterPosition;

            _selectionView.UpgradeClickEvent -= _selection.OnUpgradeTower;
            _selectionView.BuildClickEvent -= _selection.OnBuildTower;
            _selectionView.SellClickEvent -= _selection.OnSellTower;
            _selectionView.ShowPreviewEvent -= _selection.OnShowPreview;
            _selectionView.HidePreviewEvent -= _selection.OnHidePreview;

            _selection.SelectedEvent -= _selectionView.OnSelectedContent;
            _selection.CancelSelectedEvent -= _selectionView.OnCancelSelected;
            _selection.ShowTowerMenuEvent -= _selectionView.ShowTowerMenu;
            _selection.SelectToBuildEvent -= _selectionView.ShowMenuToBuild;
        }
    }
}