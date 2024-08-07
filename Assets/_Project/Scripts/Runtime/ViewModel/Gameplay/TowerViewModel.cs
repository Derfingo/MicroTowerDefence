namespace MicroTowerDefence
{
    public class TowerViewModel
    {
        private readonly IPreview _preview;
        private readonly ISelection _selection;
        private readonly IButtonsToBuildView _buttonsToBuildView;

        public TowerViewModel(IButtonsToBuildView buttonsToBuildView, ISelection selection, IPreview preview)
        {
            _preview = preview;
            _selection = selection;
            _buttonsToBuildView = buttonsToBuildView;

            _selection.OnBuildingEvent += _buttonsToBuildView.EnableButtons;

            _buttonsToBuildView.EnterPreviewTowerEvent += OnShowPreview;
            _buttonsToBuildView.ExitPreviewTowerEvent += OnHidePreview;
            _buttonsToBuildView.BuildTowerEvent += OnBuildPreview;
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

        ~TowerViewModel()
        {
            _selection.OnBuildingEvent -= _buttonsToBuildView.EnableButtons;
        }
    }
}
