using UnityEngine;

[SelectionBase]
public abstract class TileContent : GameBehaviour
{
    public Vector3 Position
    {
        get => transform.position;
        set => transform.position = value;
    }

    public abstract void Interact();
    public abstract void Undo();
    
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public override void Destroy()
    {
        Destroy(gameObject);
    }
}
