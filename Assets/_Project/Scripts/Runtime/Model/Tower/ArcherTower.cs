using UnityEngine;

namespace MicroTowerDefence
{
    public class ArcherTower : TowerBase
    {
        [SerializeField] private Transform _archer;

        private int _damage = 25;
        private float _shootPerSecond = 2.0f;
        private float _collisionRadius = 0.1f;
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

            if (IsAcquireTarget(out TargetPoint target))
            {
                if(_launchProgress >= 1f)
                {
                    var predict = PredictPosition(_archer.position, target.Position, target.Velocity, _projectileSpeed);
                    Vector3 movement = MoveParabolically(predict, _archer.position, _projectileSpeed);
                    var config = GetProjectileConfig(_elementType, _archer.position, predict, movement, _projectileSpeed, _damage, _collisionRadius); // fix radius
                                                                                                                                        //Debug.Log($"target: {target}, pridict: {predict}");
                    Shoot(config);
                    _launchProgress = 0f;
                }
            }

            return true;
        }

        protected void Shoot(ProjectileConfig config)
        {
            _projectileController.Get(ProjectileType.Arrow, Level).Initialize(config);
        }
    }
}