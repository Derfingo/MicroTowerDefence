namespace MicroTowerDefence
{
    public class InteractionPresenter
    {
        private readonly IGrid _grid;
        private readonly IRaycast _raycast;
        private readonly ISelection _selection;
        private readonly ISelectionView _selectionView;
        private readonly ITowerController _towerController;

        public InteractionPresenter(IGrid grid,
                                    IRaycast raycast,
                                    ISelection selection,
                                    ISelectionView selectionView,
                                    ITowerController towerController)
        {
            _grid = grid;
            _raycast = raycast;
            _selection = selection;
            _selectionView = selectionView;
            _towerController = towerController;

            _raycast.OnGround += _selectionView.IsEnableCursor;
            _selectionView.CellCenterPositionEvent += _grid.GetCellCenterPosition;

            _selectionView.UpgradeClickEvent += _selection.OnUpgradeTower;
            _selectionView.BuildClickEvent += _selection.OnBuildTower;
            _selectionView.SellClickEvent += _selection.OnSellTower;
            _selectionView.ShowPreviewEvent += _selection.OnShowPreview;
            _selectionView.HidePreviewEvent += _selection.OnHidePreview;
            
            _selection.OnCancelSelectedEvent += _selectionView.OnHideButtons;
            _selection.SelectToBuildEvent += _selectionView.ShowMenuToBuild;

            _towerController.OnTowerCostEvent += _selectionView.ShowTowerMenu;
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
           
            _selection.OnCancelSelectedEvent -= _selectionView.OnHideButtons;
            _selection.SelectToBuildEvent -= _selectionView.ShowMenuToBuild;

            _towerController.OnTowerCostEvent -= _selectionView.ShowTowerMenu;
        }
    }
}