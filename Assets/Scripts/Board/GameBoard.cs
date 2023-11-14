using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    [SerializeField] private Transform _ground;
    [SerializeField] private Transform _pointers;
    [SerializeField] private GameTile _tilePrefab;

    private GameTile[] _tiles;
    private BoardData _boardData;
    private TileContentFactory _contentFactory;

    private readonly Queue<GameTile> _searchFrontier = new();
    private readonly List<GameTile> _spawnPoints = new();
    private readonly List<TileContent> _contentToUpdate = new();

    public int SpawnPointCount => _spawnPoints.Count;
    public byte X => _boardData.X;
    public byte Y => _boardData.Y;

    public TileContentType[] GetAllContentTypes => _tiles.Select(t => t.Content.ContentType).ToArray();

    public void Initialize(BoardData boardData, TileContentFactory contentFactory)
    {
        _boardData = boardData;
        _contentFactory = contentFactory;
        _ground.localScale = new Vector3(_boardData.X, 1f, _boardData.Y);

        var offset = new Vector2((X - 1) * 0.5f, (Y - 1) * 0.5f);
        _tiles = new GameTile[X * Y];

        for (int i = 0, y = 0; y < Y; y++)
        {
            for (int x = 0; x < X; x++, i++)
            {
                var tile = _tiles[i] = Instantiate(_tilePrefab);
                tile.BoardPosition = new Vector2Int(x, y);
                tile.transform.SetParent(_pointers, false);
                tile.transform.localPosition = new Vector3(x - offset.x, 0f, y - offset.y);

                if (x > 0)
                    GameTile.MakeEastWestNeighbors(tile, _tiles[i - 1]);

                if (y > 0)
                    GameTile.MakeNorthSouthNeighbors(tile, _tiles[i - X]);

                tile.IsAlternative = (x & 1) == 0;
                if ((y & 1) == 0)
                {
                    tile.IsAlternative = tile.IsAlternative == false;
                }
            }
        }

        Clear();
    }

    private void SetTileContentTest()
    {
        ForceBuild(_tiles[0], _contentFactory.Get(TileContentType.Spawn));
        ForceBuild(_tiles[5], _contentFactory.Get(TileContentType.Destination));
        ForceBuild(_tiles[9], _contentFactory.Get(TileContentType.Place));
        ForceBuild(_tiles[8], _contentFactory.Get(TileContentType.Place));
        ForceBuild(_tiles[7], _contentFactory.Get(TileContentType.Place));
    }

    public void ForceBuild(GameTile tile, TileContent content)
    {
        tile.Content = content;
        _contentToUpdate.Add(content);

        if (content.ContentType == TileContentType.Spawn)
        {
            _spawnPoints.Add(tile);
        }
    }

    public bool TryBuild(GameTile tile, TileContent content)
    {
        if (tile.Content.ContentType == TileContentType.Empty)
        {
            if (content.ContentType == TileContentType.Tower)
            {
                return false;
            }
        }
        else if (tile.Content.ContentType != TileContentType.Place)
        {
            return false;
        }

        tile.Content = content;

        if (FindPath() == false)
        {
            tile.Content = _contentFactory.Get(TileContentType.Empty);
            return false;
        }

        _contentToUpdate.Add(content);

        if (content.ContentType == TileContentType.Spawn)
        {
            _spawnPoints.Add(tile);
        }
       
        return true;
    }

    public bool FindPath()
    {
        foreach (var tile in _tiles)
        {
            if (tile.Content.ContentType == TileContentType.Destination)
            {
                tile.BecomeDestination();
                _searchFrontier.Enqueue(tile);
            }
            else
            {
                tile.ClearPath();
            }
        }

        if (_searchFrontier.Count == 0)
        {
            return false;
        }

        while (_searchFrontier.Count > 0)
        {
            GameTile tile = _searchFrontier.Dequeue();
            if (tile != null)
            {
                if (tile.IsAlternative)
                {
                    _searchFrontier.Enqueue(tile.GrowPathNorth());
                    _searchFrontier.Enqueue(tile.GrowPathSouth());
                    _searchFrontier.Enqueue(tile.GrowPathEast());
                    _searchFrontier.Enqueue(tile.GrowPathWest());
                }
                else
                {
                    _searchFrontier.Enqueue(tile.GrowPathWest());
                    _searchFrontier.Enqueue(tile.GrowPathEast());
                    _searchFrontier.Enqueue(tile.GrowPathSouth());
                    _searchFrontier.Enqueue(tile.GrowPathNorth());
                }
            }
        }

        foreach (var tile in _tiles)
        {
            if (!tile.HasPath)
            {
                return false;
            }
        }

        foreach (var tile in _tiles)
        {
            tile.ShowPath();
        }

        return true;
    }

    public GameTile GetTile(Vector3 position)
    {
        var x = (int)(position.x + X * 0.5f);
        var y = (int)(position.z + Y * 0.5f);
        return GetTile(x, y);
    }

    private GameTile GetTile(int x, int y)
    {
        if (x >= 0 && x < X && y >= 0 && y < Y)
        {
            return _tiles[x + y * X];
        }
            
        return null;
    }

    public GameTile GetSpawnPoint(int index)
    {
        return _spawnPoints[index];
    }

    public void DestroyTile(GameTile tile)
    {
        if (tile.Content.ContentType <= TileContentType.Empty)
        {
            return;
        }

        _contentToUpdate.Remove(tile.Content);

        if (tile.Content.ContentType == TileContentType.Spawn)
        {
            _spawnPoints.Remove(tile);
        }

        tile.Content = _contentFactory.Get(TileContentType.Empty);
        FindPath();
    }

    public void ReplaceTile(GameTile tile, int level)
    {
        var content = tile.Content.GetComponent<Tower>().TowerType;
        var newTile = _contentFactory.Get(content, level);
        DestroyTile(tile);
        ForceBuild(tile, newTile);
        FindPath();
    }

    public void GameUpdate()
    {
        for (int i = 0; i < _contentToUpdate.Count; i++)
        {
            _contentToUpdate[i].GameUpdate();
        }
    }

    public void Clear()
    {
        _spawnPoints.Clear();
        _contentToUpdate.Clear();

        for (var i = 0; i < _boardData.Content.Length; i++)
        {
            ForceBuild(_tiles[i], _contentFactory.Get(TileContentType.Empty));
        }

        SetTileContentTest();

        FindPath();
    }
}

