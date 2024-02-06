using System;
using UnityEngine;

public class ContentSelectionView : ViewBase, IContentSelectionView
{
    [SerializeField] private GameplayButtonsView _gameplayButtonsView;
    [SerializeField] private TargetRadiusView _targetRadiusView;
    [SerializeField] private TargetCellView _targetCellView;

    public event Predicate<uint> CheckCoinsEvent;

    public event Func<bool> RaycastHitEvent;
    public event Func<TileContent> GetContentEvent;

    public event Func<Vector3> CellCenterPositionEvent;
    public event Func<Vector3Int> WorldGridPositionEvent;
    public event Func<float> HeightTilemapEvent;

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
        if (_isSelected && GetContentEvent.Invoke() == null && _previewContent == null)
        {
            OnCancelSelectedPlace();
            Debug.Log("cancel");
            return;
        }

        _targetContent = GetContentEvent.Invoke();              // func

        if (_targetContent == null && RaycastHitEvent.Invoke())
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
        _previewPosition = CellCenterPositionEvent.Invoke();             // func
        _gameplayButtonsView.ShowBuildTowerView();
    }

    private void SelectTower()
    {
        _isSelected = true;
        TowerBase tower = _targetContent.GetComponent<TowerBase>();
        SetTargetRadiusView(tower.Position, tower.TargetRange);
        _gameplayButtonsView.ShowTowerView(tower.UpgradeCost, tower.SellCost);

        var worldGridPosition = WorldGridPositionEvent.Invoke();         // func
        var heightTilemap = HeightTilemapEvent.Invoke();                 // func

        DebugView.ShowInfo(worldGridPosition, _targetContent, heightTilemap);
    }

    private void OnBuildTower(TowerType type)
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

    private void OnShowPreviewTower(TowerType type)
    {
        _input.SetUIMap();
        _previewContent = _towerFactory.Get(type);
        TowerBase tower = _previewContent.GetComponent<TowerBase>();
        var cost = _towerFactory.GetCostToBuild(tower.TowerType);

        if (CheckCoinsEvent.Invoke(cost))
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

    private void OnUpgradeTower()
    {
        UpgradeEvent?.Invoke(_targetContent);
        _gameplayButtonsView.HideButtonViews();
        _targetRadiusView.Hide();
        _targetContent = null;
        _isSelected = false;
        _input.SetPlayerMap();
    }

    private void OnSellSelectedTower()
    {
        var tower = _targetContent.GetComponent<TowerBase>();
        SellEvent?.Invoke(_targetContent, tower.SellCost);
        _targetRadiusView.Hide();
        _gameplayButtonsView.HideButtonViews();
        _targetContent = null;
        _isSelected = false;
        _input.SetPlayerMap();
    }

    private void OnPointerOverButton()
    {
        _input.SetUIMap();
    }

    private void OnPointerOutButton()
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

        var position = CellCenterPositionEvent.Invoke();     // func
        _targetCellView.UpdatePosition(position);
        _targetCellView.Display(RaycastHitEvent.Invoke());   // func
    }
}
