using UnityEngine;

namespace MicroTowerDefence
{
    public class MortarTower : TowerBase
    {
        [SerializeField] private Transform _cannon;
        [SerializeField] private Transform _cover;
        [SerializeField, Range(1f, 100f)] private float _damage = 50f;
        [SerializeField, Range(0.1f, 3f)] private float _shellBlastRadius = 1f;
        [SerializeField, Range(0.1f, 3f)] private float _shootPerSecond = 1.0f;
        [SerializeField, Range(0.1f, 5f)] private float _aimThreshold = 5f;

        private float _launchProgress = 0f;
        private bool _isAimed = false;

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

            if (IsAcquireTarget(out TargetPoint unit))
            {
                var predict = PredictPosition(_cannon.position, unit.Position, unit.Velocity);

                _isAimed = GetAngleToTarget(unit.Position) < _aimThreshold;

                RotateCover(unit.Position);
                RotateCannon(unit.Position);

                if (_isAimed && _launchProgress >= 1f)
                {
                    var config = GetProjectileConfig(_cannon.position, predict, _damage, _shellBlastRadius);
                    Shoot(config);
                    _launchProgress = 0f;
                }
            }

            return true;
        }

        private void RotateCannon(Vector3 target)
        {
            float time = 4f * Time.deltaTime;
            _cannon.rotation = Quaternion.Lerp(_cannon.rotation, Quaternion.LookRotation(target - _cannon.position), time);
            _cannon.localEulerAngles = new Vector3(_cannon.localEulerAngles.x, 0f, 0f);
        }

        private void RotateCover(Vector3 target)
        {
            Vector3 coverUp = transform.up;
            Vector3 direction = target - _cover.position;
            Vector3 flattenedCover = Vector3.ProjectOnPlane(direction, coverUp);

            _cover.rotation = Quaternion.RotateTowards(
                              Quaternion.LookRotation(_cover.forward, coverUp),
                              Quaternion.LookRotation(flattenedCover, coverUp),
                              60f * Time.deltaTime);
        }

        private void Shoot(ProjectileConfig config)
        {
            _projectileController.GetShell().Initialize(_projectileController, config);
        }

        private float GetAngleToTarget(Vector3 target)
        {
            float angle = Vector3.Angle(target - _cannon.position, _cannon.forward);
            return angle;
        }
    }
}