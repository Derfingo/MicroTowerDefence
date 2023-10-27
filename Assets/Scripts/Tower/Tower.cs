using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : GameTileContent
{
    [SerializeField, Range(1f, 10f)] protected float _targetRange = 2f;

    public abstract TowerType Type { get; }

    protected bool IsAcquireTarget(out TargetPoint target)
    {
        if (TargetPoint.FillBuffer(transform.localPosition, _targetRange))
        {
            target = TargetPoint.GetBuffered(0);
            return true;
        }

        target = null;
        return false;
    }

    protected bool IsTargetTracked(ref TargetPoint target)
    {
        if (target == null)
        {
            return false;
        }

        Vector3 myPosition = transform.localPosition;
        Vector3 targetPosition = target.Position;
        if (Vector3.Distance(myPosition, targetPosition) > _targetRange + target.ColliderSize * target.Enemy.Scale)
        {
            target = null;
            return false;
        }

        return true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 position = transform.localPosition;
        position.y += 0.1f;
        Gizmos.DrawWireSphere(position, _targetRange);
    }
}
