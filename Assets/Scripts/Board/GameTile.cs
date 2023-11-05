using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTile : MonoBehaviour
{
    [SerializeField] private Transform _pointer;

    public GameTile GrowPathNorth() => GrowPathTo(_north, Direction.South);
    public GameTile GrowPathEast() => GrowPathTo(_east, Direction.West);
    public GameTile GrowPathSouth() => GrowPathTo(_south, Direction.North);
    public GameTile GrowPathWest() => GrowPathTo(_west, Direction.East);

    public bool IsAlternative { get; set; }
    public Vector2Int BoardPosition { get; set; }
    public Vector3 ExitPoint { get; private set; }
    public Direction PathDirection { get; private set; }
    public bool HasPath => _distance != int.MaxValue;
    public GameTile NextTileOnPath => _nextOnPath;

    private int _distance;
    private GameTileContent _content;

    public GameTileContent Content
    {
        get => _content;
        set
        {
            if (_content != null)
            {
                _content.Recycle();
            }

            _content = value;
            _content.transform.localPosition = transform.localPosition;
        }
    }

    private GameTile _north, _east, _west, _south;
    private GameTile _nextOnPath;

    private Quaternion _northRotation = Quaternion.Euler(90f, 0f, 0f);
    private Quaternion _eastRotation = Quaternion.Euler(90f, 90f, 0f);
    private Quaternion _southRotation = Quaternion.Euler(90f, 180f, 0f);
    private Quaternion _westRotation = Quaternion.Euler(90f, 270f, 0f);

    public static void MakeEastWestNeighbors(GameTile east, GameTile west)
    {
        west._east = east;
        east._west = west;
    }

    public static void MakeNorthSouthNeighbors(GameTile north, GameTile south)
    {
        north._south = south;
        south._north = north;
    }

    public void ClearPath()
    {
        _distance = int.MaxValue;
        _nextOnPath = null;
    }

    public void BecomeDestination()
    {
        _distance = 0;
        _nextOnPath = null;
    }

    public void ShowPath()
    {
        if(_distance == 0)
        {
            _pointer.gameObject.SetActive(false);
            return;
        }

        _pointer.gameObject.SetActive(true);
        _pointer.localRotation =
            _nextOnPath == _north ? _northRotation :
            _nextOnPath == _east ? _eastRotation :
            _nextOnPath == _south ? _southRotation :
            _westRotation;
    }

    private GameTile GrowPathTo(GameTile neighbor, Direction direction)
    {
        if (HasPath == false || neighbor == null || neighbor.HasPath)
        {
            return null;
        }

        neighbor._distance = _distance + 1;
        neighbor._nextOnPath = this;
        neighbor.ExitPoint = neighbor.transform.localPosition + direction.GetHalfVector();
        neighbor.PathDirection = direction;
        return neighbor.Content.IsBlickingPath ? null : neighbor;
    }
}