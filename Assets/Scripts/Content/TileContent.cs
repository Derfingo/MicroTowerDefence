using UnityEngine;

[SelectionBase]
public abstract class TileContent : GameBehaviour
{
    public TileContentFactory OriginFactory { get; set; }
    public int Level { get; protected set; }
    public Vector3 Position
    {
        get => transform.position;
        set => transform.position = value;
    }

    public virtual void Initialize(TileContentFactory factory, int level)
    {
        OriginFactory = factory;
        Level = level;
    }

    public override void Recycle()
    {
        OriginFactory.Reclaim(this);
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
