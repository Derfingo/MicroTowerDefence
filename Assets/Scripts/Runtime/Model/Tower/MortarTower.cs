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

        private float _launchProgress = 0f;
        private float _aimThreshold = 5f; // 5
        private float _cannonSpeed = 6f; // 4
        private float _coverSpeed = 80f; // 60
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
                var predict = PredictPosition(_cannon.position, unit.Position, unit.Velocity, _projectileSpeed);
                _isAimed = GetAngleToTarget(predict) < _aimThreshold;

                RotateCover(predict);
                RotateCannon(predict);

                if (_isAimed && _launchProgress >= 1f)
                {
                    Vector3 movement = MoveParabolically(predict, _cannon.position, _projectileSpeed);
                    var config = GetProjectileConfig(_cannon.position, predict, movement, _projectileSpeed, _damage, _shellBlastRadius);
                    Shoot(config);
                    _launchProgress = 0f;
                }
            }

            return true;
        }

        private void RotateCannon(Vector3 target)
        {
            float time = _cannonSpeed * Time.deltaTime;
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
                              _coverSpeed * Time.deltaTime);
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