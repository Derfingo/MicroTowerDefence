using UnityEngine;

namespace MicroTowerDefence
{
    public class ArcherTower : TowerBase
    {
        [SerializeField] private Transform _archer;
        [SerializeField, Range(1f, 100f)] private float _damage = 25f;
        [SerializeField, Range(0.1f, 3f)] private float _shootPerSecond = 2.0f;

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

            while (_launchProgress >= 1f)
            {
                if (IsAcquireTarget(out TargetPoint target))
                {
                    var predict = PredictPosition(_archer.position, target.Position, target.Velocity, _projectileSpeed);
                    Vector3 movement = MoveParabolically(predict, _archer.position, _projectileSpeed);
                    var config = GetProjectileConfig(_archer.position, predict, movement, _projectileSpeed , _damage, _collisionRadius); // fix radius
                    //Debug.Log($"target: {target}, pridict: {predict}");
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

        protected void Shoot(ProjectileConfig config)
        {
            _projectileController.GetArrow().Initialize(_projectileController, config);
        }
    }
}