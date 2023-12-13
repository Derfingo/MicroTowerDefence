using UnityEngine;

public abstract class Tower : TileContent
{
    [SerializeField] private TowerType _towerType;
    [SerializeField, Range(1f, 10f)] protected float _targetRange = 2f;
    [SerializeField, Range(50, 500)] protected uint _cost;
    public TowerType TowerType => _towerType;
    public uint Cost => _cost;

    private TowerConfig _config;

    public override void Initialize(TileContentFactory factory, int level)
    {
        base.Initialize(factory, level);
        SetConfig(OriginFactory.GetConfig(level));
    }

    public void SetConfig(TowerConfig config)
    {
        _config = config;
        SetStats(_config);
    }

    protected abstract void SetStats(TowerConfig config);

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

public enum TowerType : byte
{
    Beam = 101,
    Mortar = 102,
    Archer = 103,
    Magic = 104,
}
