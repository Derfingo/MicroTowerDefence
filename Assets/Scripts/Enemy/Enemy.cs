using UnityEngine;
using static EnemyFactory;

public class Enemy : GameBehaviour
{
    [SerializeField] private PathMovement _movement;
    [SerializeField] private Transform _model;
    [SerializeField] private EnemyView _view;

    public EnemyFactory OriginFactory { get; set; }
    public float Health { get; private set; }
    public float Scale { get; private set; }

    private PathPointsView _pathPoints;

    private const float CHANGE_DIR_SPEED_MULTIPLIER = 0.8f;
    private uint _coins;

    private Vector3 _positionFrom, _positionTo;
    private float _progress, _progressFactor;

    private Direction _direction;
    private DirectionChange _directionChange;
    private float _directionAngleFrom, _directionAngleTo;
    private float _pathOffset;
    private float _speed;
    private float _originalSpeed;

    public void Initialize(EnemyConfig config)
    {
        _model.localScale *= config.Scale.RandomValueInRange;
        _pathOffset = config.PathOffset.RandomValueInRange;
        _originalSpeed = config.Speed.RandomValueInRange;
        _speed = config.Speed.RandomValueInRange;
        Health = config.Health.RandomValueInRange;
        Scale = config.Scale.RandomValueInRange;
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

        if (Health <= 0f)
        {
            DisableView();
            _view.Die();
            FindAnyObjectByType<Coins>().Add(_coins);
            return false;
        }

        if (_movement.IsFinish)
        {
            Recycle();
            return false;
        }

        _movement.Move();

        //_progress += Time.deltaTime * _progressFactor;
        //while (_progress >= 1)
        //{
        //    //if (_tileTo == null)
        //    //{
        //    //    InitializationGame.EnemyReachedDestination();
        //    //    Recycle();
        //    //    return false;
        //    //}

        //    _progress = (_progress - 1f) / _progressFactor;
        //    _progress *= _progressFactor;
        //}

        //if (_directionChange == DirectionChange.None)
        //{
        //    transform.localPosition = Vector3.LerpUnclamped(_positionFrom, _positionTo, _progress);
        //}
        //else
        //{
        //    float angle = Mathf.LerpUnclamped(_directionAngleFrom, _directionAngleTo, _progress);
        //    transform.localRotation = Quaternion.Euler(0f, angle, 0f);
        //}

        return true;
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
    }

    private void PrepareForward()
    {
        transform.localRotation = _direction.GetRotation();
        _directionAngleTo = _direction.GetAngle();
        _model.localPosition = new Vector3(_pathOffset, 0f);
        _progressFactor = _speed;
    }

    private void PrepareTurnRight()
    {
        _directionAngleTo = _directionAngleFrom + 90f;
        _model.localPosition = new Vector3(_pathOffset - 0.5f, 0f);
        transform.localPosition = _positionFrom + _direction.GetHalfVector();
        _progressFactor = _speed * CHANGE_DIR_SPEED_MULTIPLIER;
    }

    private void PrepareTurnLeft()
    {
        _directionAngleTo = _directionAngleFrom - 90f;
        _model.localPosition = new Vector3(_pathOffset + 0.5f, 0f);
        transform.localPosition = _positionFrom + _direction.GetHalfVector();
        _progressFactor = _speed * CHANGE_DIR_SPEED_MULTIPLIER;
    }

    private void PrepareTurnAround()
    {
        _directionAngleTo = _directionAngleFrom + (_pathOffset < 0f ? 180f : -180f);
        _model.localPosition = new Vector3(_pathOffset, 0f);
        transform.localPosition = _positionFrom;
        _progressFactor = _speed * CHANGE_DIR_SPEED_MULTIPLIER;
    }

    public override void Recycle()
    {
        OriginFactory.Reclaim(this);
    }

    private void DisableView()
    {
        _view.GetComponentInChildren<Collider>().enabled = false;
        _view.GetComponentInChildren<TargetPoint>().enabled = false;
    }
}

public enum EnemyType
{
    Small,
    Medium,
    Large,
    Slime,
}
