using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : GameTileContent
{
    [SerializeField, Range(1f, 10f)] protected float _targetRange = 2f;
    [SerializeField] protected TowerLevelConfig _towerLevelConfig;
    [SerializeField] protected Transform _towerModel;

    public new abstract GameTileContentType Type { get; }
    public abstract void Initialize(TowerLevel level);

    public TowerLevel CurrentLevel = TowerLevel.First;

    public void SetLevel(TowerLevel level)
    {
        _towerModel.GetChild((int)level - 1).gameObject.SetActive(false);
        _towerModel.GetChild((int)level).gameObject.SetActive(true);
        SetConfig(level);
        CurrentLevel = level;
    }

    public void SetConfig(TowerLevel level)
    {
        var config = _towerLevelConfig.GetConfig(level);
        _targetRange = config.TargetTange;
    }

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

        float distance = Vector3.Distance(myPosition, targetPosition);
        float range = _targetRange + target.ColliderSize * target.Enemy.Scale;

        if (distance > range || target.IsEnabled == false)
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
