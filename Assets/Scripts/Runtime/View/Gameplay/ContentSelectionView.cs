using System;
using UnityEngine;

public class ContentSelectionView : ViewBase, IContentSelectionView
{
    [SerializeField] private TilemapController _tilemapController;
    [SerializeField] private RaycastController _raycast;
    [SerializeField] private Coins _coins;
    [Space]
    [SerializeField] private GameplayButtonsView _gameplayButtonsView;
    [SerializeField] private TargetRadiusView _targetRadiusView;
    [SerializeField] private TargetCellView _targetCellView;

    public event Action<TileContent, Vector3> BuildEvent;
    public event Action<TileContent, uint> SellEvent;
    public event Action<TileContent> UpgradeEvent;

    private TowerFactory _towerFactory;
    private TileContent _previewContent;
    private TileContent _targetContent;
    private Vector3 _previewPosition;
    private IInputActions _input;
    private bool _isSelected;

    public void Initialize(IInputActions input, TowerFactory towerFactory)
    {
        _input = input;
        _towerFactory = towerFactory;

        _input.SelectPlaceEvent += OnSelectPlace;
        _input.CancelSelectPlaceEvent += OnCancelSelectedPlace;

        _gameplayButtonsView.BuildTowerEvent += OnBuildTower;
        _gameplayButtonsView.SellTowerEvent += OnSellSelectedTower;
        _gameplayButtonsView.UpgradeTowerEvent += OnUpgradeTower;

        _gameplayButtonsView.EnterPreviewTowerEvent += OnShowPreviewTower;
        _gameplayButtonsView.ExitPreviewTowerEvent += OnHidePreviewTower;

        _gameplayButtonsView.PointerEnterEvent += OnPointerOverButton;
        _gameplayButtonsView.PointerExitEvent += OnPointerOutButton;

        _targetCellView.Show();
    }

    public void GameUpdate()
    {
        UpdateTargetCellView();
    }

    private void OnSelectPlace()
    {
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

    private void OnCancelSelectedPlace()
    {
        _gameplayButtonsView.HideButtonViews();
        _targetRadiusView.Hide();
        _targetContent = null;
        _isSelected = false;
        _input.SetPlayerMap();
    }

    private void SelectToBuild()
    {
        _isSelected = true;
        _previewPosition = _tilemapController.GetCellCenterPosition();
        _gameplayButtonsView.ShowBuildTowerView();
    }

    private void SelectTower()
    {
        _isSelected = true;
        TowerBase tower = _targetContent.GetComponent<TowerBase>();
        var position = tower.Position;
        position.y += 0.01f;
        SetTargetRadiusView(position, tower.TargetRange);
        _gameplayButtonsView.ShowTowerView(tower.UpgradeCost, tower.SellCost);

        DebugView.ShowInfo(_tilemapController.GridPosition, _targetContent, _tilemapController.HeightTilemap);
    }

    public void OnBuildTower(TowerType type)
    {
        if (_previewContent != null)
        {
            BuildEvent?.Invoke(_previewContent, _previewPosition);
            _previewContent = null;
            _targetRadiusView.Hide();
            _gameplayButtonsView.HideButtonViews();
        }
        else
        {
            Debug.Log("preview is null");
        }

        _input.SetPlayerMap();
        _isSelected = false;
    }

    public void OnShowPreviewTower(TowerType type)
    {
        _input.SetUIMap();
        _previewContent = _towerFactory.Get(type);
        TowerBase tower = _previewContent.GetComponent<TowerBase>();
        var cost = _towerFactory.GetCostToBuild(tower.TowerType);

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

    public void OnHidePreviewTower(TowerType type)
    {
        if (_previewContent != null)
        {
            _previewContent.Destroy();
            _previewContent = null;
        }

        _input.SetPlayerMap();
    }

    public void OnUpgradeTower()
    {
        UpgradeEvent?.Invoke(_targetContent);
        _gameplayButtonsView.HideButtonViews();
        _targetRadiusView.Hide();
        _targetContent = null;
        _isSelected = false;
        _input.SetPlayerMap();
    }

    public void OnSellSelectedTower()
    {
        SellEvent?.Invoke(_targetContent, _targetContent.GetComponent<TowerBase>().SellCost);
        _targetRadiusView.Hide();
        _gameplayButtonsView.HideButtonViews();
        _targetContent = null;
        _isSelected = false;
        _input.SetPlayerMap();
    }

    public void OnPointerOverButton()
    {
        _input.SetUIMap();
    }

    public void OnPointerOutButton()
    {
        _input.SetPlayerMap();
    }

    private void SetTargetRadiusView(Vector3 position, float radius)
    {
        _targetRadiusView.SetRadiusView(position, radius);
        _targetRadiusView.Show();
    }

    private void UpdateTargetCellView()
    {
        if (_isSelected)
        {
            return;
        }

        var position = _tilemapController.GetCellPosition();
        _targetCellView.UpdatePosition(position);
        _targetCellView.Display(_raycast.IsHit);
    }
}
