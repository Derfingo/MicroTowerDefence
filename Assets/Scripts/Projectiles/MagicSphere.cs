using UnityEngine;

public class MagicSphere : Projectile
{
    private Vector3 _launchPoint, _targetPoint;
    private float _damage, _velocity;

    public void Initialize(Vector3 launchPoint, Vector3 targetPosition, float velocity, float damage)
    {
        _launchPoint = launchPoint;
        _targetPoint = targetPosition;
        _velocity = velocity;
        _damage = damage;
    }

    public override bool GameUpdate()
    {
        transform.localPosition = Vector3.Lerp(_targetPoint, _launchPoint, Time.deltaTime);

        if (transform.position.y <= 0.1f)
        {
            OriginFactory.Reclaim(this);
            return false;
        }

        if (Vector3.Distance(_launchPoint, _targetPoint) < 0.1f)
        {
            TargetPoint.FillBuffer(_targetPoint, 0.1f);

            for (int i = 0; i < TargetPoint.BufferedCount; i++)
            {
                TargetPoint.GetBuffered(i).Enemy.TakeDamage(_damage);
            }
        }

        return true;
    }
}
