using UnityEngine;

public class Shell : Projectile
{
    public override bool GameUpdate()
    {
        if (DetectGround())
        {
            _projectile.GetExplosion().Initialize(transform.position, _blastRadious, _damage);
            OriginFactory.Reclaim(this);
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

    private bool DetectGround()
    {
        if (transform.position.y <= 0.2f)
        {
            return true;
        }

        return false;
    }
}
