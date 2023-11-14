using UnityEngine;

public class BeamTower : Tower
{
    [SerializeField] private Transform _turret;
    [SerializeField] private Transform _laserBeam;
    [SerializeField, Range(1f, 100f)] private float _damagePerSecond = 10f;

    private Vector3 _laserBeamScale;
    private TargetPoint _target;

    private void Awake()
    {
        _laserBeamScale = _laserBeam.localScale;
    }

    protected override void SetStats(TowerConfig config)
    {
        _damagePerSecond = config.DamagePerSecond;
    }

    public override void GameUpdate()
    {
        if (IsTargetTracked(ref _target) || IsAcquireTarget(out _target))
        {
            Shoot();
        }
        else
        {
            _laserBeam.localScale = Vector3.zero;
        }
    }

    private void Shoot()
    {
        var point = _target.Position;
        _turret.LookAt(point);
        _laserBeam.localRotation = _turret.localRotation;

        var distacne = Vector3.Distance(_turret.position, point);
        _laserBeamScale.z = distacne;
        _laserBeam.localScale = _laserBeamScale;
        _laserBeam.localPosition = _turret.localPosition + 0.5f * distacne * _laserBeam.forward;
        _target.Enemy.TakeDamage(_damagePerSecond * Time.deltaTime);
    }
}
