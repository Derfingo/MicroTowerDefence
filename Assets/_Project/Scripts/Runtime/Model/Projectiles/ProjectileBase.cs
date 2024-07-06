using UnityEngine;

namespace MicroTowerDefence
{
    [SelectionBase]
    public abstract class ProjectileBase : GameBehaviour
    {
        protected Rigidbody _rigidbBody;
        protected ElementType _elementType;

        private Collider _collider;

        protected Vector3 _startPosition;
        protected Vector3 _targetPosition;
        protected Vector3 _movement;

        protected float _blastRadious;
        protected int _damage;
        protected float _speed;
        protected bool _isMoving;

        protected int _groundLayer = 6;
        protected int _enemyLayer = 9;
        protected int _shieldLayer = 12;

        public void Initialize(ProjectileConfig config)
        {
            _collider = GetComponent<Collider>();
            _elementType = config.Element;
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
            //_rigidbBody.isKinematic = true;
            _collider.enabled = false;
            _isMoving = false;
            Destroy(gameObject, delay);
        }

        protected abstract void Move();
        protected abstract void Collide(Collision collision, int layerIndex);

        private void OnCollisionEnter(Collision collision)
        {
            Collide(collision, collision.gameObject.layer);
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
        public ElementType Element;
        public Vector3 StartPosition;
        public Vector3 TargetPosition;
        public Vector3 Movement;
        public float Velocity;
        public float BlastRadius;
        public int Damage;

        public ProjectileConfig(ElementType type, Vector3 startPosition, Vector3 targetPosition, Vector3 movement, float velocity, int damage, float blastRadius = 0f)
        {
            Element = type;
            StartPosition = startPosition;
            TargetPosition = targetPosition;
            BlastRadius = blastRadius;
            Movement = movement;
            Velocity = velocity;
            Damage = damage;
        }
    }
}