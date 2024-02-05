using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapController : MonoBehaviour
{
    [SerializeField] private ActionMapReader _input;
    [SerializeField] private RaycastController _raycast;
    [SerializeField] private Tilemap[] _tilemapArray;
    [SerializeField] private TowerPlaceView[] _towerPlaces;

    public float HeightTilemap { get; private set; }
    public Vector3Int GridPosition { get; private set; }

    private Dictionary<float, Tilemap> _tilemaps;
    private Tilemap _targetTilemap;
    private bool _isShow;

    public void Initialize()
    {
        InitializeTilemaps();
        HideTowerPlaces();
        _input.TowerPlacesEvent += OnTowerPlaces;
    }

    public void GameUpdate()
    {
        var position = _raycast.GetPosition();
        DetectPosition(position);
    }

    public Vector3 GetCellCenterPosition()
    {
        return _targetTilemap.GetCellCenterWorld(GridPosition);
    }

    public Vector3 GetCellPosition()
    {
        return _targetTilemap.CellToWorld(GridPosition);
    }

    private void OnTowerPlaces()
    {
        _isShow = !_isShow;

        if (_isShow)
        {
            ShowTowerPlaces();
        }
        else
        {
            HideTowerPlaces();
        }
    }

    private void ShowTowerPlaces()
    {
        foreach (var place in _towerPlaces)
        {
            place.Show();
        }
    }

    private void HideTowerPlaces()
    {
        foreach (var place in _towerPlaces)
        {
            place.Hide();
        }
    }

    private void InitializeTilemaps()
    {
        _tilemaps = new Dictionary<float, Tilemap>();

        for (int i = 0; i < _tilemapArray.Length; i++)
        {
            var height = _tilemapArray[i].transform.position.y;
            _tilemaps.Add(height, _tilemapArray[i]);
        }

        _targetTilemap = _tilemapArray[0];
    }

    private void DetectPosition(Vector3 position)
    {
        if (GetTilamap(position.y))
        {
            GetGridPosition(position);
        }
    }

    private bool GetTilamap(float mouseHeight)
    {
        //var height = Mathf.Round(mouseHeight);

        if (_tilemaps.ContainsKey(mouseHeight))
        {
            HeightTilemap = mouseHeight;
            _targetTilemap = _tilemaps[mouseHeight];
            return true;
        }

        return false;
    }

    private void GetGridPosition(Vector3 position)
    {
        GridPosition = _targetTilemap.WorldToCell(position);
    }
}
