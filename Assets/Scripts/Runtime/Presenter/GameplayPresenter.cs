using UnityEngine;

public class GameplayPresenter : MonoBehaviour
{
    private IGameplayButtonsView _gameplayButtonsView;
    private IContentSelectionView _contentSelectionView;
    private ITowerControllerModel _towerControllerModel;

    private IPathView _pathView;

    private IGridModel _gridModel;

    private IRaycastModel _raycastModel;

    private ICoinsModel _coins;

    public void Initialize(IGameplayButtonsView gameplayButtonsView,
                           IContentSelectionView contentSelection,
                           ITowerControllerModel towerController,
                           IPathView pathView,
                           IGridModel grid,
                           IRaycastModel raycast,
                           ICoinsModel coins)
    {
        _coins = coins;
        _gridModel = grid;
        _pathView = pathView;
        _raycastModel = raycast;

        _gameplayButtonsView = gameplayButtonsView;
        _contentSelectionView = contentSelection;
        _towerControllerModel = towerController;

        _contentSelectionView.CellCenterPositionEvent += _gridModel.GetCellCenterPosition;
        _contentSelectionView.WorldGridPositionEvent += _gridModel.GetWorldGridPosition;
        _contentSelectionView.HeightTilemapEvent += _gridModel.GetHeightTilemap;

        _contentSelectionView.GetContentEvent += _raycastModel.GetContent;
        _contentSelectionView.RaycastHitEvent += _raycastModel.CheckHit;
        _contentSelectionView.CheckCoinsEvent += _coins.Check;

        _contentSelectionView.UpgradeEvent += _towerControllerModel.OnUpgrade;
        _contentSelectionView.BuildEvent += _towerControllerModel.OnBuild;
        _contentSelectionView.SellEvent += _towerControllerModel.OnSell;
    }
}
