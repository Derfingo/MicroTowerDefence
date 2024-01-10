using UnityEngine;

[SelectionBase]
public abstract class Projectile : GameBehaviour
{
    public ProjectileFactory OriginFactory { get; set; }

    public override void Recycle()
    {
        OriginFactory.Reclaim(this);
    }
}
