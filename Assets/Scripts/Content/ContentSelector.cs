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
    public event Action OnUpdate;
    public event Action OnSell;

    public TileContent TargetContent { get; private set; }

    private TileContentFactory _contentFactory;

    private void Start()
    {
        _view.OnSelectBuilding += OnSelectedBuilding;
        _view.OnSellBuilding += OnSellSelectedBuilding;
        _view.OnUpgradeBuilding += OnUpgradeBuilding;

        _input.OnMouseButtonDown += OnGetContent;
        _input.OnMouseButtonUp += OnBuildSelected;
    }

    private void _view_OnUpgradeBuilding()
    {
        throw new NotImplementedException();
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
        if (TargetContent != null)
        {
            if (_raycast.IsHit)
            {
                var centerCellPosition = _tilemapController.GetCellCenterPosition();
                OnBuild?.Invoke(TargetContent, centerCellPosition);
                TargetContent = null;
            }
            else
            {
                TargetContent.Recycle();
            }
            
            _view.ShowBuildingMenu();
        }
    }

    private void OnSelectedBuilding(TowerType type)
    {
        TargetContent = _contentFactory.Get(type);
    }

    private void OnUpgradeBuilding()
    {
        _view.ShowBuildingMenu();
        OnUpdate?.Invoke();
    }

    private void OnSellSelectedBuilding()
    {
        _view.ShowBuildingMenu();
        OnSell?.Invoke();
    }

    private void OnGetContent()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            var content = _raycast.GetContent();

            if (content != null)
            {
                _view.ShowTowerMenu();
            }
            else
            {
                _view.ShowBuildingMenu();
            }

            DebugView.ShowInfo(_tilemapController.GridPosition, content, _tilemapController.HeightTilemap);
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
        if (TargetContent == null)
        {
            return;
        }

        if (_raycast.IsHit)
        {
            TargetContent.Show();
        }
        else
        {
            TargetContent.Hide();
        }

        var viewPosition = _raycast.GetPosition();
        viewPosition.y = _tilemapController.HeightTilemap;
        TargetContent.transform.position = viewPosition;
    }
}
