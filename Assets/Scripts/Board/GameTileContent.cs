using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTileContent : MonoBehaviour
{
    [SerializeField] private GameTileContentType _type;

    public GameTileContentType Type => _type;

    public GameTileContentFactory OriginFactory { get; set; }

    public void Recycle()
    {
        OriginFactory.Reclaim(this);
    }
}

public enum GameTileContentType : byte
{
    Empty = 0,
    Destination = 1,
    Wall = 51,
    Spawn = 2
}
