namespace MicroTowerDefence
{
    public class TowerBuildingViewModel
    {
        private readonly IPrepare _prepare;
        private readonly IPreview _preview;
        private readonly ITowerCost _towerCost;
        private readonly ISelection _selection;
        private readonly ITowerBuildingView _towerBuilding;

        public TowerBuildingViewModel(ITowerBuildingView towerBuildig, 
                                      ISelection selection, 
                                      IPreview preview, 
                                      IPrepare prepare,
                                      ITowerCost towerCost)
        {
            _prepare = prepare;
            _preview = preview;
            _towerCost = towerCost;
            _selection = selection;
            _towerBuilding = towerBuildig;

            _selection.OnBuildingEvent += _towerBuilding.EnableButtons;
            _towerBuilding.EnterPreviewTowerEvent += OnShowPreview;
            _towerBuilding.ExitPreviewTowerEvent += OnHidePreview;
            _towerBuilding.BuildTowerEvent += OnBuildPreview;
            _prepare.OnPrepareEvent += OnPrepareCost;
        }

        private void OnShowPreview(TowerType type)
        {
            _preview.OnShowPreview(type, _selection.GridPosition);
        }

        private void OnHidePreview(TowerType type)
        {
            _preview.OnHidePreview();
        }

        private void OnBuildPreview(TowerType type)
        {
            _preview.OnBuilPreview();
        }

        private void OnPrepareCost()
        {
            _towerBuilding.SetTowersCost(_towerCost.GetAllCostTowers());
        }

        ~TowerBuildingViewModel()
        {
            _selection.OnBuildingEvent -= _towerBuilding.EnableButtons;
            _towerBuilding.EnterPreviewTowerEvent -= OnShowPreview;
            _towerBuilding.ExitPreviewTowerEvent -= OnHidePreview;
            _towerBuilding.BuildTowerEvent -= OnBuildPreview;
            _prepare.OnPrepareEvent -= OnPrepareCost;
        }
    }
}
