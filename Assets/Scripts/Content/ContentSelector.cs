using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContentSelector : MonoBehaviour
{
    [SerializeField] private TilemapController _tilemapController;
    [SerializeField] private TargetCellView _targetCellView;
    [SerializeField] private RaycastController _raycast;
    [SerializeField] private InputController _input;
    [SerializeField] private ViewController _view;

    public event Action<TileContent, Vector3> OnBuild;
    public event Action<TileContent> OnUpdate;
    public event Action<TileContent> OnSell;

    private TileContentFactory _contentFactory;
    private TileContent _previewContent;
    private TileContent _targetContent;

    private void Start()
    {
        _view.OnSelectBuilding += OnSelectedBuilding;
        _view.OnSellBuilding += OnSellSelectedBuilding;
        _view.OnUpgradeBuilding += OnUpgradeBuilding;

        _input.OnMouseButtonDown += OnGetContent;
        _input.OnMouseButtonUp += OnBuildSelected;
    }

    public void Initialize(TileContentFactory contentFactory)
    {
        _contentFactory = contentFactory;
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
                _previewContent.Recycle();
            }
            
            _view.ShowBuildingMenu();
        }
    }

    private void OnSelectedBuilding(TowerType type)
    {
        _previewContent = _contentFactory.Get(type);
    }

    private void OnUpgradeBuilding()
    {
        _view.ShowBuildingMenu();
        OnUpdate?.Invoke(_targetContent);
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
                _view.ShowTowerMenu();
            }
            else
            {
                _view.ShowBuildingMenu();
            }

            DebugView.ShowInfo(_tilemapController.GridPosition, _targetContent, _tilemapController.HeightTilemap);
        }
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
        }
        else
        {
            _previewContent.Hide();
        }

        var viewPosition = _raycast.GetPosition();
        viewPosition.y = _tilemapController.HeightTilemap;
        _previewContent.transform.position = viewPosition;
    }
}
