using UnityEngine;

namespace MicroTowerDefence
{
    public class MagicTower : TowerBase
    {
        [SerializeField] private Transform _spere;

        private int _damage = 25;
        private float _shootPerSecond = 2.0f;
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
                    var predict = PredictPosition(_spere.position, target.Position, target.Velocity, _projectileSpeed);
                    Vector3 movement = MoveLinear(predict, _spere.position, _projectileSpeed);
                    var config = GetProjectileConfig(_elementType, _spere.position, predict, movement, _projectileSpeed, _damage, 0f);
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
            _projectileController.Get(ProjectileType.Sphere, Level).Initialize(config);
        }
    }
}