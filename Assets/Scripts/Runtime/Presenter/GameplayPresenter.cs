using UnityEngine;

public class GameplayPresenter : MonoBehaviour
{
    private IGameplayButtonsView _gameplayButtonsView;
    private IContentSelectionView _contentSelectionView;

    private IPathView _pathView;

    private IGridModel _gridModel;

    private IRaycastModel _raycastModel;

    private ICoinsModel _coins;

    public void Initialize(IGameplayButtonsView gameplayButtonsView,
                           IContentSelectionView contentSelectionModel,
                           IPathView pathView,
                           IGridModel gridModel,
                           IRaycastModel raycastModel,
                           ICoinsModel coins)
    {
        _coins = coins;
        _pathView = pathView;
        _gridModel = gridModel;
        _raycastModel = raycastModel;

        _gameplayButtonsView = gameplayButtonsView;
        _contentSelectionView = contentSelectionModel;

        _contentSelectionView.CellCenterPositionEvent += _gridModel.GetCellCenterPosition;
        _contentSelectionView.WorldGridPositionEvent += _gridModel.GetWorldGridPosition;
        _contentSelectionView.HeightTilemapEvent += _gridModel.GetHeightTilemap;

        _contentSelectionView.GetContentEvent += _raycastModel.GetContent;
        _contentSelectionView.RaycastHitEvent += _raycastModel.CheckHit;
        _contentSelectionView.CheckCoinsEvent += _coins.Check;
    }
}
