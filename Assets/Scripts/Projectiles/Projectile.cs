using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : GameBehaviour
{
    public ProjectileFactory OriginFactory { get; set; }

    public override void Recycle()
    {
        OriginFactory.Reclaim(this);
    }
}
