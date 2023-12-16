using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public abstract class TileContent : MonoBehaviour
{
    public TileContentFactory OriginFactory { get; set; }
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

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
