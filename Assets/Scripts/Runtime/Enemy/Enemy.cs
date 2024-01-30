using System;
using UnityEngine;
using static EnemyFactory;

public class Enemy : GameBehaviour
{
    [SerializeField] private PathMovement _movement;
    [SerializeField] private Transform _model;
    [SerializeField] private EnemyView _view;

    public event Action<uint> OnFinish;
    public event Action<uint> OnDie;
    public EnemyFactory OriginFactory { get; set; }
    public Vector3 Velocity => _movement.Velocity;

    private PathPointsView _pathPoints;

    private float _health;
    public float Scale { get; private set; }
    private float _speed;
    private uint _coins;
    private uint _damage;
    private float _originalSpeed;

    public void Initialize(EnemyConfig config)
    {
        _model.localScale *= config.Scale.RandomValueInRange;
        _originalSpeed = config.Speed.RandomValueInRange;
        _speed = config.Speed.RandomValueInRange;
        _health = config.Health.RandomValueInRange;
        Scale = config.Scale.RandomValueInRange;
        _damage = config.Damage;
        _coins = config.Coins;
        _view.Initialize(this);
        SetSpeed(_speed);
    }

    public void SetPath(PathPointsView pathPoints)
    {
        _pathPoints = pathPoints;
        _movement.Initialize(_pathPoints, MovementType.Move, _pathPoints.LeastDistance, _speed);
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
            Destroy();
            OnFinish?.Invoke(_damage);
            OnDie = null;
            return false;
        }

        _movement.Move();

        return true;
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
    }

    private void DisableView()
    {
        _view.GetComponentInChildren<Collider>().enabled = false;
        _view.GetComponentInChildren<TargetPoint>().enabled = false;
    }

    public override void Destroy()
    {
        Destroy(gameObject);
    }
}

public enum EnemyType
{
    Small,
    Medium,
    Large,
    Slime,
}
