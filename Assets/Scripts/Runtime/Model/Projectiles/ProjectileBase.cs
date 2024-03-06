using UnityEngine;

namespace MicroTowerDefence
{
    [SelectionBase]
    public abstract class ProjectileBase : GameBehaviour
    {
        protected ProjectileController _projectile;
        protected Rigidbody _rigidbBody;

        protected Vector3 _middlePoint;
        protected Vector3 _startPosition;
        protected Vector3 _targetPosition;
        protected Vector3 _movement;

        protected float _blastRadious;
        protected float _middleY = 0.9f;  // 0.9f
        protected float _damage;
        protected float _speed;

        protected bool _isMoving;
        protected int _groundLayer = 6;
        protected int _enemyLayer = 9;

        public void Initialize(ProjectileController projectile, ProjectileConfig config)
        {
            _projectile = projectile;
            _damage = config.Damage;
            _startPosition = config.StartPosition;
            _targetPosition = config.TargetPosition;
            _blastRadious = config.BlastRadius;
            _movement = config.Movement;
            _speed = config.Velocity;
            transform.position = config.StartPosition;
            SetupRigidBody(config.Movement);
        }

        public override void Reclaim(float delay = 0f)
        {
            _rigidbBody.isKinematic = true;
            _isMoving = false;
            Destroy(gameObject, delay);
        }

        protected abstract void Move();
        protected abstract void DetectGround(Collision collision);
        protected abstract void DetectEnemy(Collision collision);

        private void OnCollisionEnter(Collision collision)
        {
            DetectGround(collision);
            DetectEnemy(collision);
        }

        private void SetupRigidBody(Vector3 velocity)
        {
            _rigidbBody = GetComponent<Rigidbody>();
            _rigidbBody.velocity = velocity;
            _isMoving = true;
        }
    }

    public struct ProjectileConfig
    {
        public Vector3 StartPosition;
        public Vector3 TargetPosition;
        public Vector3 Movement;
        public float Velocity;
        public float BlastRadius;
        public float Damage;

        public ProjectileConfig(Vector3 startPosition, Vector3 targetPosition, Vector3 movement, float velocity, float damage, float blastRadius = 0f)
        {
            StartPosition = startPosition;
            TargetPosition = targetPosition;
            BlastRadius = blastRadius;
            Movement = movement;
            Velocity = velocity;
            Damage = damage;
        }
    }
}