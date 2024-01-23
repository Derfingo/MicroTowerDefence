using UnityEngine;

public class MortarTower : TowerBase
{
    [SerializeField] private Transform _cannon;
    [SerializeField, Range(1f, 100f)] private float _damage = 50f;
    [SerializeField, Range(0.1f, 3f)] private float _shellBlastRadius = 1f;
    [SerializeField, Range(0.1f, 3f)] private float _shootPerSecond = 1.0f;

    private float _launchProgress;

    protected override void SetStats(TowerConfig config)
    {
        _damage = config.Damage;
        _targetRange = config.TargetRange;
        _shootPerSecond = config.ShootPerSecond;
        _shellBlastRadius = config.ShellBlastRadius;
    }

    public override bool GameUpdate()
    {
        _launchProgress += Time.deltaTime * _shootPerSecond;

        while (_launchProgress >= 1f)
        {
            if (IsAcquireTarget(out TargetPoint target))
            {
                var predict = PredictPosition(_cannon.position, target.Position, target.Velocity);
                var config = GetProjectileConfig(_cannon.position, predict, _damage, _shellBlastRadius);
                RotateCannon(predict);
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

    private void RotateCannon(Vector3 target)
    {
        Vector3 direction = target - _cannon.position;
        _cannon.forward = direction;
    }

    private void Shoot(ProjectileConfig config)
    {
        _projectileController.GetShell().Initialize(_projectileController, config);
    }
}
