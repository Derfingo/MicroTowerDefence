using UnityEngine;

public class ArcherTower : TowerBase
{
    [SerializeField] private Transform _archer;
    [SerializeField, Range(1f, 100f)] private float _damage = 25f;
    [SerializeField, Range(0.1f, 3f)] private float _shootPerSecond = 2.0f;

    private float _collisionRadius = 0.1f;
    private float _launchSpeed;
    private float _launchProgress;

    protected override void SetStats(TowerConfig config)
    {
        _damage = config.Damage;
        _targetRange = config.TargetRange;
        _shootPerSecond = config.ShootPerSecond;
    }

    public override bool GameUpdate()
    {
        _launchProgress += Time.deltaTime * _shootPerSecond;

        while (_launchProgress >= 1f)
        {
            if (IsAcquireTarget(out TargetPoint target))
            {
                var predict = PredictPosition(_archer.position, target.Position, target.Velocity);
                var config = GetProjectileConfig(_archer.position, predict, _damage, _collisionRadius); // fix radius
                Shoot(config);
                _launchProgress -= 1f;
            }
            else
            {
                _launchProgress = 0.999f;
            }
        }

        return true;
    }

    private void Shoot(ProjectileConfig config)
    {
        _projectile.GetArrow().Initialize(_projectile, config);
    }

    private void Launch(TargetPoint target)
    {
        Vector3 launchPoint = _archer.position;
        Vector3 targetPoint = target.Position;
        targetPoint.y = 0f;

        Vector2 direction;
        direction.x = targetPoint.x - launchPoint.x;
        direction.y = targetPoint.z - launchPoint.z;

        float x = direction.magnitude;
        float y = -launchPoint.y;
        direction /= x;

        float g = 9.81f;
        float s = _launchSpeed;
        float s2 = s * s;

        float r = s2 * s2 - g * (g * x * x + 2f * y * s2);
        r = Mathf.Max(0f, r);

        float tanTheta = (s2 + Mathf.Sqrt(r)) / (g * x);
        float cosTheta = Mathf.Cos(Mathf.Atan(tanTheta));
        float sinTheta = cosTheta * tanTheta;

        _archer.localRotation = Quaternion.LookRotation(new Vector3(direction.x, tanTheta, direction.y));

        //_projectile.SpawnArrow().Initialize(_projectile ,launchPoint, targetPoint,
            //new Vector3(s * cosTheta * direction.x, s * sinTheta, s * cosTheta * direction.y), _shellBlastRadius, _damage);
    }
}
