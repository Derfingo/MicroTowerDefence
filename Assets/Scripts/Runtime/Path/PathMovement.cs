using UnityEngine;

public class PathMovement : MonoBehaviour
{
    private PathPointsView _pathPointsView;
    private MovementType _movementType;
    private float _leastDistance;
    private Vector3 _target;
    private int _indexPoint;
    private float _speed;
    private Vector3 _velocity;

    public bool IsFinish { get; private set; }
    public Vector3 Velocity => _velocity;

    public void Initialize(PathPointsView pathPointsView, MovementType type, float maxDistance, float speed)
    {
        _pathPointsView = pathPointsView;
        _leastDistance = maxDistance;
        _movementType = type;
        _speed = speed;

        _target = _pathPointsView.Points[1].position;
    }

    private void Update()
    {
        var aim = _target - transform.position;

        if (aim.magnitude > 0.5f)
        {
            _velocity = aim.normalized * _speed;
        }
        else
        {
            _velocity = Vector3.zero;
        }
    }

    public void Move()
    {
        Vector3 direction = _target - transform.position;
        transform.forward = direction;

        if (Vector3.Distance(transform.position, _target) <= _leastDistance)
        {
            GetNextPoint();
        }

        if (_movementType == MovementType.Move)
        {
            transform.Translate(direction.normalized * _speed * Time.deltaTime, Space.World);
        }
        else if (_movementType == MovementType.Lerp)
        {
            transform.position = Vector3.Lerp(direction.normalized, _target, Time.deltaTime * _speed);
        }

        //var angle = Mathf.Lerp(transform.position.y, _angle, Time.deltaTime / 2);
        //transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    private void GetNextPoint()
    {
        if (_indexPoint >= _pathPointsView.Points.Length - 1)
        {
            IsFinish = true;
            return;
        }

        _indexPoint++;
        _target = _pathPointsView.Points[_indexPoint].position;
    }
}

public enum MovementType
{
    Move,
    Lerp
}
