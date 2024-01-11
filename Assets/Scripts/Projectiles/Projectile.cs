using UnityEngine;

[SelectionBase]
public abstract class Projectile : GameBehaviour
{
    protected ProjectileController _projectile;
    protected Vector3 _middlePoint;
    protected Vector3 _startPosition;
    protected Vector3 _targetPosition;
    protected float _damage;
    protected float _blastRadious;
    protected float _middleY = 0.9f;
    protected float _speed = 4f;

    public void Initialize(ProjectileController projectile, ProjectileConfig config)
    {
        _projectile = projectile;
        _damage = config.Damage;
        _startPosition = config.StartPosition;
        _targetPosition = config.TargetPosition;
        _blastRadious = config.BlastRadius;
        transform.position = config.StartPosition;
        SetMiddlePoint();

        //Debug.Log($"target: {_targetPosition}, pridict: {_predictPosition}");
    }

    protected abstract void Move();
    protected abstract void Rotate();

    private void SetMiddlePoint()
    {
        _middlePoint = _targetPosition / 2f;
        _middlePoint.y = (_targetPosition - _startPosition).magnitude * _middleY;
        //Debug.Log("height: " + _middlePoint.y);
    }

    public override void Recycle()
    {
        OriginFactory.Reclaim(this);
    }
}

public struct ProjectileConfig
{
    public Vector3 StartPosition;
    public Vector3 TargetPosition;
    public float BlastRadius;
    public float Damage;

    public ProjectileConfig(Vector3 startPosition, Vector3 targetPosition, float damage, float blastRadius = 0f)
    {
        StartPosition = startPosition;
        TargetPosition = targetPosition;
        BlastRadius = blastRadius;
        Damage = damage;
    }
}
