using UnityEngine;

public class Arrow : Projectile
{
    [SerializeField] private AnimationCurve _curve;
    private Vector3 _middlePoint;
    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private float _damage;
    private float _blastRadious = 0.2f;
    private float _middleY = 0.9f;
    private float _speed = 4f;

    public void Initialize(Vector3 start, Vector3 target, float damage)
    {
        _damage = damage;
        _startPosition = start;
        _targetPosition = target;

        transform.position = start;

        SetMiddlePoint();

        //Debug.Log($"target: {_targetPosition}, pridict: {_predictPosition}");
    }

    public override bool GameUpdate()
    {
        if (DetectCollision())
        {
            OriginFactory.Reclaim(this);
            return false;
        }

        if (DetectGround())
        {
            OriginFactory.Reclaim(this, 2f);
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

    private bool DetectCollision()
    {
        if (TargetPoint.FillBuffer(transform.position, _blastRadious))
        {
            TargetPoint.GetBuffered(0).Enemy.TakeDamage(_damage);
            return true;
        }

        return false;
    }

    private bool DetectGround()
    {
        if (transform.position.y <= 0f)
        {
            return true;
        }

        return false;
    }
}
