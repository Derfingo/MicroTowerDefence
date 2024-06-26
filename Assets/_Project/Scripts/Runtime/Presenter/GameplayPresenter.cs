namespace MicroTowerDefence
{
    public class GameplayPresenter : UnityEngine.MonoBehaviour
    {
        private IGameplayButtonsView _gameplayButtonsView;
        private ISelectionView _selectionView;
        private ITowerController _towerController;
        private ISelection _selection;

        private IPathView _pathView;

        private IGrid _gridModel;

        private IRaycast _raycastModel;

        private ICoins _coins;

        public void Initialize(IGameplayButtonsView gameplayButtonsView,
                               ISelectionView contentSelection,
                               ITowerController towerController,
                               ISelection selection,
                               IPathView pathView,
                               IGrid grid,
                               IRaycast raycast,
                               ICoins coins)
        {
            _coins = coins;
            _gridModel = grid;
            _pathView = pathView;
            _raycastModel = raycast;

            _gameplayButtonsView = gameplayButtonsView;
            _selectionView = contentSelection;
            _towerController = towerController;
            _selection = selection;

        }
    }
}