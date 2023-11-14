using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class TileContent : MonoBehaviour
{
    [SerializeField] protected TileContentType _contentType;
    public TileContentType ContentType => _contentType;
    public TileContentFactory OriginFactory { get; set; }
    public bool IsBlickingPath => ContentType == TileContentType.Wall || ContentType == TileContentType.Tower;
    public int Level { get; protected set; }

    public virtual void Initialize(TileContentFactory factory, int level)
    {
        OriginFactory = factory;
        Level = level;
    }

    public void Recycle()
    {
        OriginFactory.Reclaim(this);
    }

    public virtual void GameUpdate()
    {
    }
}

public enum TileContentType : byte
{
    Destination = 1,
    Place = 3,
    Empty = 4,
    Spawn = 5,

    Wall = 51,
    Tower = 61
}
