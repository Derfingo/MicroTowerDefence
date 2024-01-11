using UnityEngine;

public abstract class GameBehaviour : MonoBehaviour
{
    public ProjectileFactory OriginFactory { get; set; }

    public virtual bool GameUpdate() => true;

    public abstract void Recycle();
}
