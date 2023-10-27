using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class GameTileContent : MonoBehaviour
{
    [SerializeField] private GameTileContentType _type;

    public GameTileContentType Type => _type;

    public GameTileContentFactory OriginFactory { get; set; }

    public bool IsBlickingPath => Type == GameTileContentType.Wall || Type == GameTileContentType.Tower;

    public void Recycle()
    {
        OriginFactory.Reclaim(this);
    }

    public virtual void GameUpdate()
    {

    }
}

public enum GameTileContentType : byte
{
    Empty = 0,
    Destination = 1,
    Wall = 51,
    Spawn = 2,
    Tower = 61
}

public enum TowerType
{
    Laser,
    Ballista
}
