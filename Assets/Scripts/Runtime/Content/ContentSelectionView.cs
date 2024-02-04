using System;
using UnityEngine;

public class ContentSelectionView : MonoBehaviour
{
    [SerializeField] private TilemapController _tilemapController;
    [SerializeField] private RaycastController _raycast;
    [SerializeField] private Coins _coins;
    [Space]
    [SerializeField] private GameplayViewController _view;
    [SerializeField] private TargetCellView _targetCellView;
    [SerializeField] private TargetRadiusView _targetRadiusView;

    public event Action<TileContent, Vector3> BuildEvent;
    public event Action<TileContent> UpgradeEvent;
    public event Action<TileContent, uint> SellEvent;

    private TowerFactory _towerFactory;
    private TileContent _previewContent;
    private TileContent _targetContent;
    private Vector3 _previewPosition;
    private IInputActions _input;

    private bool _isSelected;

    public void Initialize(TowerFactory towerFactory, IInputActions input)
    {
        _towerFactory = towerFactory;
        _input = input;

        _input.SelectPlaceEvent += OnSelectPlace;
        _input.CancelSelectPlaceEvent += OnCancelSelectedPlace;

        _view.BuildTowerEvent += OnBuildTower;
        _view.SellTowerEvent += OnSellSelectedTower;
        _view.UpgradeTowerEvent += OnUpgradeTower;

        _view.SelectPreviewTowerEvent += OnShowPreviewTower;
        _view.DeselectPreviewTowerEvent += OnHidePreviewTower;

        _view.PointerEnterEvent += OnPointerOverButton;
        _view.PointerExitEvent += OnPointerOutButton;

        _targetCellView.Show();
        _view.HideMenus();
    }

    public void GameUpdate()
    {
        SetTargetCellView();
    }

    private void OnBuildTower(TowerType type)
    {
        if (_previewContent != null)
        {
            BuildEvent?.Invoke(_previewContent, _previewPosition);
            _previewContent = null;
            _targetRadiusView.Hide();
            _view.HideMenus();
        }
        else
        {
            Debug.Log("preview is null");
        }

        _input.SetPlayerMap();
        _isSelected = false;
    }

    private void OnSelectPlace()
    {
        /*
         * 1. place -> build tower -> track mouse
         * 2. tower -> update or sell > track mouse
         * 3. trap -> interact -> track mouse
         * 4. track mouse
        */

        if (_isSelected && _raycast.GetContent() == null && _previewContent == null)
        {
            OnCancelSelectedPlace();
            Debug.Log("cancel");
            return;
        }

        _targetContent = _raycast.GetContent();

        if (_targetContent == null && _raycast.IsHit)
        {
            SelectToBuild();
        }
        else if (_targetContent != null)
        {
            SelectTower();
        }
    }

    private void SelectToBuild()
    {
        _isSelected = true;
        _previewPosition = _tilemapController.GetCellCenterPosition();
        _view.ShowBuildingMenu();
    }

    private void SelectTower()
    {
        _isSelected = true;
        TowerBase tower = _targetContent.GetComponent<TowerBase>();
        var position = tower.Position;
        position.y += 0.01f;
        SetTargetRadiusView(position, tower.TargetRange);
        _view.ShowTowerMenu(tower.UpgradeCost, tower.SellCost);

        DebugView.ShowInfo(_tilemapController.GridPosition, _targetContent, _tilemapController.HeightTilemap);
    }

    private void OnShowPreviewTower(TowerType type)
    {
        _input.SetUIMap();
        _previewContent = _towerFactory.Get(type);
        TowerBase tower = _previewContent.GetComponent<TowerBase>();
        var cost = _towerFactory.GetCost(tower.TowerType);

        if (_coins.Check(cost))
        {
            _previewContent.Position = _previewPosition;
            _previewContent.Show();
            SetTargetRadiusView(tower.Position, tower.TargetRange);
        }
        else
        {
            _previewContent.Destroy();
            // show not enough coins
        }
    }

    private void OnHidePreviewTower(TowerType type)
    {
        if (_previewContent != null)
        {
            _previewContent.Destroy();
            _previewContent = null;
        }

        _input.SetPlayerMap();
    }

    private void OnCancelSelectedPlace()
    {
        _view.HideMenus();
        _targetRadiusView.Hide();
        _targetContent = null;
        _isSelected = false;
        _input.SetPlayerMap();
    }

    private void OnUpgradeTower()
    {
        UpgradeEvent?.Invoke(_targetContent);
        _view.HideMenus();
        _targetRadiusView.Hide();
        _targetContent = null;
        _isSelected = false;
        _input.SetPlayerMap();
    }

    private void OnSellSelectedTower()
    {
        SellEvent?.Invoke(_targetContent, _targetContent.GetComponent<TowerBase>().SellCost);
        _targetRadiusView.Hide();
        _view.HideMenus();
        _targetContent = null;
        _isSelected = false;
        _input.SetPlayerMap();
    }

    private void SetTargetRadiusView(Vector3 position, float radius)
    {
        _targetRadiusView.SetRadiusView(position, radius);
        _targetRadiusView.Show();
    }

    private void SetTargetCellView()
    {
        if (_isSelected)
        {
            return;
        }

        var viewPosition = _tilemapController.GetCellPosition();
        _targetCellView.SetTargetCellPosition(viewPosition);

        if (_raycast.IsHit)
        {
            _targetCellView.ShowTargetCell();
        }
        else
        {
            _targetCellView.HideTargerCell();
        }
    }

    private void OnPointerOverButton()
    {
        _input.SetUIMap();
    }

    private void OnPointerOutButton()
    {
        _input.SetPlayerMap();
    }
}
