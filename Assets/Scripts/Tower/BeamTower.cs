using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BeamTower : Tower
{
    [SerializeField] private Transform _turret;
    [SerializeField] private Transform _laserBeam;
    [SerializeField, Range(1f, 100f)] private float _damagePerSecond = 10f;

    public override GameTileContentType Type => GameTileContentType.Laser;

    private Vector3 _laserBeamScale;
    private TargetPoint _target;

    private void Awake()
    {
        _laserBeamScale = _laserBeam.localScale;
    }

    public override void Initialize(TowerLevel level)
    {
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
