using System;
using UnityEngine;

namespace MicroTowerDefence
{
    [SelectionBase]
    public abstract class TowerBase : TileContent
    {
        [SerializeField] private TowerType _towerType;
        [SerializeField, Range(1f, 10f)] protected float _targetRange;

        protected float _projectileSpeed = 4f;

        private TargetRadiusView _targetRadiusView;

        public event Action<TileContent> SelectedEvent;

        public readonly uint MaxLevel = 2;
        public TowerType TowerType => _towerType;
        public float TargetRange => _targetRange;
        public uint Level { get; protected set; }
        public uint UpgradeCost { get; protected set; }
        public uint SellCost { get; protected set; }

        protected ProjectileController _projectileController;
        protected float _predictTime;

        private BoxCollider _collider;

        public bool IsInit
        {
            get => _collider.enabled;
            set => _collider.enabled = value;
        }

        public void Initialize(TowerConfig config, uint level)
        {
            Level = level;
            UpgradeCost = config.UpgradeCost;
            SellCost = config.SellCost;
            SetStats(config);
            _collider = GetComponent<BoxCollider>();
            _targetRadiusView = GetComponentInChildren<TargetRadiusView>();
            _collider.size = new Vector3(0.9f, 1f, 0.9f);
            _collider.enabled = false;
        }

        public void SetProjectile(ProjectileController projectile)
        {
            _projectileController = projectile;
        }

        public override void Interact()
        {
            SelectedEvent?.Invoke(this);
            ShowTargetRadius(true);
        }

        public override void Undo()
        {
            ShowTargetRadius(false);
        }

        public void ShowTargetRadius(bool isEnable)
        {
            if (isEnable)
            {
                _targetRadiusView.SetRadius(_targetRange);
                _targetRadiusView.Show();
            }
            else
            {
                _targetRadiusView.Hide();
            }
        }

        protected abstract void SetStats(TowerConfig config);

        protected Vector3 MoveParabolically(Vector3 predict, Vector3 shootPoint, float projectileVelocity)
        {
            var aim = predict - shootPoint;
            aim.y = 0f;
            float antiGravity = -Physics.gravity.y * _predictTime / 2;
            float deltaY = (predict.y - shootPoint.y) / _predictTime;
            Vector3 velocity = aim.normalized * projectileVelocity;
            velocity.y = antiGravity + deltaY;
            return velocity;
        }

        protected Vector3 MoveLinear(Vector3 predict, Vector3 shootPoint, float projectileVelocity)
        {
            var aim = predict - shootPoint;
            Vector3 velocity = aim.normalized * projectileVelocity;
            return velocity;
        }

        protected bool IsAcquireTarget(out TargetPoint target)
        {
            if (TargetPoint.FillBuffer(transform.localPosition, _targetRange))
            {
                target = TargetPoint.GetBuffered(0);
                return true;
            }

            target = null;
            return false;
        }

        protected bool IsTargetTracked(ref TargetPoint target)
        {
            if (target == null)
            {
                return false;
            }

            Vector3 myPosition = transform.localPosition;
            Vector3 targetPosition = target.Position;

            float distance = Vector3.Distance(myPosition, targetPosition);
            float range = _targetRange + target.ColliderSize * target.Enemy.Scale;

            if (distance > range || target.IsEnabled == false)
            {
                target = null;
                return false;
            }

            return true;
        }

        protected Vector3 PredictPosition(Vector3 startPosition, Vector3 targetPosition, Vector3 targetVelocity, float projectileVelocity)
        {
            Vector3 targetDistance = targetPosition - startPosition;

            float a = Vector3.Dot(targetVelocity, targetVelocity) - projectileVelocity * projectileVelocity;
            float b = 2 * Vector3.Dot(targetDistance, targetVelocity);
            float c = Vector3.Dot(targetDistance, targetDistance);

            float discriminant = b * b - 4 * a * c;
            if (discriminant < 0.1f) return Vector3.zero;

            float sqrt = Mathf.Sqrt(discriminant);

            float t1 = (-b + sqrt) / (2 * a);
            float t2 = (-b - sqrt) / (2 * a);

            if (t1 < 0f && t2 < 0f) return Vector3.zero;
            else if (t1 < 0f) _predictTime = t2;
            else if (t2 < 0f) _predictTime = t1;
            else _predictTime = Mathf.Max(t1, t2);

            Vector3 result = targetPosition + targetVelocity * _predictTime;
            return result;
        }

        protected ProjectileConfig GetProjectileConfig(Vector3 start, Vector3 target, Vector3 movement, float velocity, int damage, float blastRadius)
        {
            return new ProjectileConfig(start, target, movement, velocity, damage, blastRadius);
        }
    }
}