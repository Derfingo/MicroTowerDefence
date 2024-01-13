using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContentSelector : MonoBehaviour
{
    [SerializeField] private TilemapController _tilemapController;
    [SerializeField] private RaycastController _raycast;
    [SerializeField] private InputController _input;
    [SerializeField] private ViewController _view;
    [Space]
    [SerializeField] private TargetCellView _targetCellView;
    [SerializeField] private TargetRadiusView _targetRadiusView;

    public event Action<TileContent, Vector3> OnBuild;
    public event Action<TileContent> OnUpdate;
    public event Action<TileContent> OnSell;

    private TowerFactory _towerFactory;
    private TileContent _previewContent;
    private TileContent _targetContent;

    private void Start()
    {
        _view.OnSelectBuilding += OnSelectedBuilding;
        _view.OnSellBuilding += OnSellSelectedBuilding;
        _view.OnUpgradeBuilding += OnUpgradeBuilding;

        _input.OnMouseButtonDown += OnGetContent;
        _input.OnMouseButtonUp += OnBuildSelected;

        _targetCellView.Show();
    }

    public void Initialize(TowerFactory towerFactory)
    {
        _towerFactory = towerFactory;
    }

    private void Update()
    {
        SetTargetCellView();
        SetTargetContentView();
    }

    private void OnBuildSelected()
    {
        if (_previewContent != null)
        {
            if (_raycast.IsHit)
            {
                var centerCellPosition = _tilemapController.GetCellCenterPosition();
                OnBuild?.Invoke(_previewContent, centerCellPosition);
                _previewContent = null;
            }
            else
            {
                _previewContent.Destroy();
            }
            
            _view.ShowBuildingMenu();
            _targetRadiusView.Hide();
        }
    }

    private void OnSelectedBuilding(TowerType type)
    {
        _previewContent = _towerFactory.Get(type);
        TowerBase tower = _previewContent.GetComponent<TowerBase>();
        SetTargetRange(tower.Position, tower.TargetRange);
    }

    private void OnUpgradeBuilding()
    {
        OnUpdate?.Invoke(_targetContent);
        _view.ShowBuildingMenu();
        _targetRadiusView.Hide();
        _targetContent = null;
    }

    private void OnSellSelectedBuilding()
    {
        _view.ShowBuildingMenu();
        OnSell?.Invoke(_targetContent);
        _targetContent = null;
    }

    private void OnGetContent()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            _targetContent = _raycast.GetContent();

            if (_targetContent != null)
            {
                TowerBase tower = _targetContent.GetComponent<TowerBase>();
                var position = tower.Position;
                position.y += 0.01f;
                SetTargetRange(position, tower.TargetRange);
                _view.ShowTowerMenu();
            }
            else
            {
                _view.ShowBuildingMenu();
                _targetRadiusView.Hide();
            }

            DebugView.ShowInfo(_tilemapController.GridPosition, _targetContent, _tilemapController.HeightTilemap);
        }
    }

    private void SetTargetRange(Vector3 position, float radius)
    {
        _targetRadiusView.SetRadiusView(position, radius);
        _targetRadiusView.Show();
    }

    private void SetTargetCellView()
    {
        var viewPosition = _tilemapController.GetCellPosition();
        _targetCellView.SetPosition(viewPosition);

        if (_raycast.IsHit)
        {
            _targetCellView.Show();
        }
        else
        {
            _targetCellView.Hide();
        }
    }

    private void SetTargetContentView()
    {
        if (_previewContent == null)
        {
            return;
        }

        if (_raycast.IsHit)
        {
            _previewContent.Show();
            _targetRadiusView.Show();
        }
        else
        {
            _previewContent.Hide();
            _targetRadiusView.Hide();
        }

        var viewPosition = _raycast.GetPosition();
        viewPosition.y = _tilemapController.HeightTilemap;
        _previewContent.transform.position = viewPosition;
        _targetRadiusView.SetRadiusView(viewPosition);
    }
}
