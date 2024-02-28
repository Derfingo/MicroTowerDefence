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

        protected abstract void Move();
        protected abstract void Rotate();

        private void SetMiddlePoint()
        {
            _middlePoint = _targetPosition / 2f; // 2f
            _middlePoint.y = (_targetPosition - _startPosition).magnitude * _middleY;
            //Debug.Log("height: " + _middlePoint.y);
        }

        private void SetupRigidBody(Vector3 velocity)
        {
            _rigidbBody = GetComponent<Rigidbody>();
            
            if (_rigidbBody.isKinematic == false)
            {
                _rigidbBody.velocity = velocity;
            }
        }

        public override void Destroy()
        {
            Destroy(gameObject);
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