using UnityEngine;

namespace MicroTowerDefence
{
    public class TowerTest : TowerBase
    {
        [SerializeField] private TowerFactory _factory;
        [SerializeField] private EnemyTest _enemy;
        [SerializeField] private ProjectileController _projectiles;
        [Space]
        [SerializeField] private Transform _cannon;
        [SerializeField] private Transform _cover;

        private float _collisionRadius = 0.2f; // 0.1
        private float _damage;
        float _shootDelay = _delayReset;
        static float _delayReset = 1f;
        private float _coverSpeedRotation = 12.0f;

        private TowerConfig _config;

        private void Awake()
        {
            _config = new TowerConfig
            {
                TargetRange = 5f,
                Damage = 1,
                ShellBlastRadius = 5,
                ShootPerSecond = 2,
                DamagePerSecond = 1,
            };
            Initialize(_config, 0);
            SetProjectile(_projectiles);
            IsInit = true;
        }

        private void Update()
        {
            GameUpdate();
        }

        protected override void SetStats(TowerConfig config)
        {
            _damage = config.Damage;
            _targetRange = config.TargetRange;
        }

        public override bool GameUpdate()
        {
            _shootDelay -= Time.deltaTime;

            RotateCover();
            float? angle = CalculateAngle();
            RotateCannon(angle);

            if (angle != null && _shootDelay <= 0.0f)
            {
                var predict = PredictPosition(_cannon.position, _enemy.transform.position, _enemy.Velocity, _projectileSpeed);
                Vector3 movement = MoveParabolically(predict, _cannon.position, _projectileSpeed);
                var config = GetProjectileConfig(_cannon.position, predict, movement, _projectileSpeed, _damage, _collisionRadius);
                Shoot(config);
                _shootDelay = _delayReset;
            }

            return true;
        }

        protected void Shoot(ProjectileConfig config)
        {
            var arrow = _projectileController.GetArrow();
            arrow.Initialize(_projectiles, config);
        }

        private void RotateCover()
        {
            Vector3 direction = (_enemy.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
            _cover.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _coverSpeedRotation);
        }

        private void RotateCannon(float? angle)
        {
            if (angle != null)
            {
                _cannon.localEulerAngles = new Vector3(360.0f - (float)angle, 0f, 0f);
            }
        }

        private float? CalculateAngle()
        {
            Vector3 targetDir = _enemy.transform.position - _cannon.transform.position;
            float y = targetDir.y;
            targetDir.y = 0.0f;
            float x = targetDir.magnitude - 0f; // default - 1, rotation by X
            float gravity = 9.8f;
            float sSqr = _projectileSpeed * _projectileSpeed;
            float underTheSqrRoot = (sSqr * sSqr) - gravity * (gravity * x * x + 2 * y * sSqr);

            if (underTheSqrRoot >= 0f) // default >= 0f
            {
                float root = Mathf.Sqrt(underTheSqrRoot);
                float highAngle = sSqr + root;
                float lowAngle = sSqr - root;

                //if (low) return (Mathf.Atan2(lowAngle, gravity * x) * Mathf.Rad2Deg);
                //else return (Mathf.Atan2(highAngle, gravity * x) * Mathf.Rad2Deg);

                return Mathf.LerpAngle(Mathf.Atan2(highAngle, gravity * x) * Mathf.Rad2Deg,
                                       Mathf.Atan2(lowAngle, gravity * x) * Mathf.Rad2Deg,
                                       x);
            }
            else
                return null;
        }
    }
}