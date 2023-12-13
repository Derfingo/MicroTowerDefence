using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapController : MonoBehaviour, IBuilder
{
    [SerializeField] private InputController _input;
    [SerializeField] private RaycastController _racast;
    [SerializeField] private Tilemap[] _tilemapArray;
    [Space]
    [SerializeField] private TargetCellView _targetCellView;

    private Dictionary<int, Tilemap> _tilemaps;
    private Vector3Int _gridPosition;
    private TileContent _targetContent;
    private Tilemap _targetTilemap;
    private TileBase _targetTile;
    private int _heightTilemap;

    private void Start()
    {
        //_input.OnMouseButtonDown += GetTile;
        InitializeTilemaps();
    }

    private void Update()
    {
        var position = _racast.RaycastPosition();
        DetectPosition(position);
    }

    public void Build(TileContent content)
    {
        // check place
        // build
    }

    public void Destroy(TileContent content)
    {
        // reclaim tile
    }

    private void InitializeTilemaps()
    {
        _tilemaps = new Dictionary<int, Tilemap>();

        for (int i = 0; i < _tilemapArray.Length; i++)
        {
            int height = (int)_tilemapArray[i].transform.position.y;
            _tilemaps.Add(height, _tilemapArray[i]);
        }

        _targetTilemap = _tilemapArray[0];
    }

    private bool GetTile()
    {
        _targetTile = _targetTilemap.GetTile(_gridPosition);
        if (_targetTile == null)
        {
            return false;
        }

        DebugView.ShowInfo(_gridPosition, _targetTile, _heightTilemap);
        return true;
    }

    private void DetectPosition(Vector3 position)
    {
        if (GetTilamap(position.y))
        {
            GetGridPosition(position);
            SetTargetCellView();
        }
    }

    private bool GetTilamap(float mouseHeight)
    {
        var height = Mathf.RoundToInt(mouseHeight);

        if (_tilemaps.ContainsKey(height))
        {
            _heightTilemap = height;
            _targetTilemap = _tilemaps[height];
            return true;
        }

        return false;
    }

    private void GetGridPosition(Vector3 position)
    {
        _gridPosition = _targetTilemap.WorldToCell(position);
    }

    private void SetTargetCellView()
    {
        var viewPosition = _targetTilemap.CellToWorld(_gridPosition);
        _targetCellView.SetPosition(viewPosition);

        if (_racast.IsHit)
        {
            _targetCellView.Show();
        }
        else
        {
            _targetCellView.Hide();
        }
    }
}
