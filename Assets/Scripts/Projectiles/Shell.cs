using UnityEngine;

public class Shell : Projectile
{
    ProjectileController _projectile;
    private Vector3 _middlePoint;
    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private float _damage;
    private float _blastRadious;
    private float _middleY = 0.9f;
    private float _speed = 4f;

    public void Initialize(ProjectileController projectile ,Vector3 start, Vector3 target, float blastRadius, float damage)
    {
        _damage = damage;
        _startPosition = start;
        _targetPosition = target;
        _projectile = projectile;
        _blastRadious = blastRadius;

        transform.position = start;

        SetMiddlePoint();

        //Debug.Log($"target: {_targetPosition}, pridict: {_predictPosition}");
    }

    public override bool GameUpdate()
    {
        if (DetectGround())
        {
            _projectile.GetExplosion().Initialize(transform.position, _blastRadious, _damage);
            OriginFactory.Reclaim(this);
            return false;
        }

        MoveByBezier();
        Rotate();

        return true;
    }

    private void SetMiddlePoint()
    {
        _middlePoint = _targetPosition / 2f;
        _middlePoint.y = (_targetPosition - _startPosition).magnitude * _middleY;
        //Debug.Log("height: " + _middlePoint.y);
    }

    private void MoveByBezier()
    {
        float delta = _speed * Time.deltaTime;

        _middlePoint = Vector3.MoveTowards(_middlePoint, _targetPosition, delta);
        transform.position = Vector3.MoveTowards(transform.position, _middlePoint, delta);
    }

    private void Rotate()
    {
        Vector3 relativePosition = _middlePoint - transform.position;
        if (relativePosition == Vector3.zero)
        {
            return;
        }
        Quaternion rotation = Quaternion.LookRotation(relativePosition, Vector3.up);
        transform.rotation = rotation;
    }

    private bool DetectGround()
    {
        if (transform.position.y <= 0.2f)
        {
            return true;
        }

        return false;
    }
}
