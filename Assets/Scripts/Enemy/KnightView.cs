using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightView : EnemyView
{
    public void OnDieAnimationFinished()
    {
        _enemy.Recycle();
    }
}
