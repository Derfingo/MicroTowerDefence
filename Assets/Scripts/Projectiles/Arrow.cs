using UnityEngine;

public class Arrow : Projectile
{
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

        Move();
        Rotate();

        return true;
    }

    protected override void Move()
    {
        MoveByBezier();
    }

    protected override void Rotate()
    {
        Vector3 relativePosition = _middlePoint - transform.position;
        if (relativePosition == Vector3.zero)
        {
            return;
        }
        Quaternion rotation = Quaternion.LookRotation(relativePosition, Vector3.up);
        transform.rotation = rotation;
    }

    private void MoveByBezier()
    {
        float delta = _speed * Time.deltaTime;

        _middlePoint = Vector3.MoveTowards(_middlePoint, _targetPosition, delta);
        transform.position = Vector3.MoveTowards(transform.position, _middlePoint, delta);
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
