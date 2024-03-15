using UnityEngine;

namespace MicroTowerDefence
{
    public class BeamTower : TowerBase
    {
        [SerializeField] private Transform _turret;
        [SerializeField] private Transform _laserBeam;

        private float _launchProgress;
        private float _shootPerDelay = 0.2f;
        private int _damagePerSecond = 10;
        private Vector3 _laserBeamScale;
        private TargetPoint _target;

        private void Awake()
        {
            _laserBeamScale = _laserBeam.localScale;
        }

        protected override void SetStats(TowerConfig config)
        {
            _targetRange = config.TargetRange;
            _damagePerSecond = config.DamagePerSecond;
        }

        public override bool GameUpdate()
        {
            _launchProgress += Time.deltaTime;

            if (IsTargetTracked(ref _target) || IsAcquireTarget(out _target))
            {
                Shoot();
            }
            else
            {
                _laserBeam.localScale = Vector3.zero;
            }

            return true;
        }

        private void Shoot()  // fix
        {
            var point = _target.Position;
            _turret.LookAt(point);
            _laserBeam.localRotation = _turret.localRotation;

            var distacne = Vector3.Distance(_turret.position, point);
            _laserBeamScale.z = distacne;
            _laserBeam.localScale = _laserBeamScale;
            _laserBeam.localPosition = _turret.localPosition + 0.5f * distacne * _laserBeam.forward;

            if (_launchProgress >= _shootPerDelay)
            {
                _target.Enemy.TakeDamage(_damagePerSecond);
                _launchProgress = 0f;
            }
        }
    }
}