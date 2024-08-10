using System;
using UnityEngine;

namespace MicroTowerDefence
{
    [SelectionBase]
    public abstract class EnemyBase : GameBehaviour, IDamage
    {
        [SerializeField] private Transform _model;
        private TargetPoint _targetPoint;
        private PathMovement _movement;
        private EnemyViewBase _view;
        private Collider _collider;
        private ElementType _elementType;

        public event Action<uint> OnFinish;
        public event Action<uint> OnDie;
        public EnemyFactory OriginFactory { get; set; }
        public Vector3 Velocity => _movement.Velocity;

        private float _health;
        public float Scale { get; private set; }
        private float _speed;
        private uint _coins;
        private uint _damage;
        private float _originalSpeed;

        public virtual void Initialize(EnemyConfig config)
        {
            GetComponents();
            SetStats(config);
            SetSpeed(_speed);
        }

        public void SetPath(PathConfig config)
        {
            _movement.Initialize(config.Points, config.MovementType, config.LeastDistance, _speed);
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetSpeed(float factor)
        {
            _speed = _originalSpeed * factor;
            _view.SetSpeedFactor(factor);
        }

        public void SetPause(bool isPause)
        {
            _view.Animator.enabled = isPause;
        }

        public void TakeDamage(float value, ElementType type)
        {
            float factor = ElementFactor.GetFactor(type, _elementType);
            float damage = value * factor;
            _health -= damage;
            //print($"value: {value} factor: {factor} damage: {damage}");
        }

        public override bool GameUpdate()
        {
            if (_view.IsInited == false)
            {
                return true;
            }

            if (_health <= 0f)
            {
                DisableView();
                _view.Die();
                OnDie.Invoke(_coins);
                OnFinish = null;
                return false;
            }

            if (_movement.IsFinish)
            {
                Reclaim();
                OnFinish?.Invoke(_damage);
                OnDie = null;
                return false;
            }

            _movement.Move();

            return true;
        }

        private void DisableView()
        {
            _collider.enabled = false;
            _targetPoint.enabled = false;
            //_view.GetComponentInParent<Collider>().enabled = false;
            //_view.GetComponentInParent<TargetPoint>().enabled = false;
        }

        public override void Reclaim(float delay = 0f)
        {
            Destroy(gameObject);
        }

        private void GetComponents()
        {
            _movement = GetComponentInChildren<PathMovement>();
            _view = GetComponentInChildren<EnemyViewBase>();
            _targetPoint = GetComponent<TargetPoint>();
            _collider = GetComponent<Collider>();
            _view.Initialize(this);
        }

        private void SetStats(EnemyConfig config)
        {
            _elementType = config.Element;
            _model.localScale *= config.Scale;
            _originalSpeed = config.Speed;
            _speed = config.Speed;
            _health = (int)config.Health;
            Scale = config.Scale;
            _damage = config.Damage;
            _coins = config.Coins;
        }
    }
}