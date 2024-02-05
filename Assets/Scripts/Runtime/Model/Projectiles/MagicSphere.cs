using UnityEngine;

public class MagicSphere : ProjectileBase
{
    public override bool GameUpdate()
    {
        if (DetectCollision())
        {
            Destroy();
            return false;
        }

        if (DetectGround())
        {
            Destroy();
            return false;
        }

        Move();

        return true;
    }

    protected override void Move()
    {
        float deltaTime = _speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, deltaTime);
    }

    protected override void Rotate()
    {
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
        if (transform.position.y <= 0.1f)
        {
            return true;
        }

        return false;
    }
}
