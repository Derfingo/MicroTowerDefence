using UnityEngine;

public class MagicTower : TowerBase
{
    [SerializeField] private Transform _spere;
    [SerializeField, Range(0.2f, 3f)] private float _shootPerSecond = 2.0f;
    [SerializeField, Range(1f, 100f)] private float _damage = 25f;

    private float _launchProgress;

    protected override void SetStats(TowerConfig config)
    {
        _damage = config.Damage;
        _shootPerSecond = config.ShootPerSecond;
    }

    public override bool GameUpdate()
    {
        _launchProgress += Time.deltaTime * _shootPerSecond;

        while (_launchProgress >= 1f)
        {
            if (IsAcquireTarget(out TargetPoint target))
            {
                Launch(target);
                _launchProgress -= 1f;
            }
            else
            {
                _launchProgress = 0.999f;
            }
        }

        return true;
    }

    private void Launch(TargetPoint target)
    {
        Vector3 launchPoint = _spere.position;
        Vector3 targetPoint = target.Position;
        //_projectile.GetSphere().Initialize(launchPoint, targetPoint, 1f, _damage);
    }
}
