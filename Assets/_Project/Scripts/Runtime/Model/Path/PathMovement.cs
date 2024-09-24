using UnityEngine;

namespace MicroTowerDefence
{
    public class PathMovement : MonoBehaviour, IPath
    {
        private MovementType _movementType;
        private Transform[] _pathPoints;
        private Vector3 _target;
        private Vector3 _velocity;
        private float _maxDistance;
        private float _speed;
        private int _indexPoint;

        private readonly float _speedRotation = 15f;

        public bool IsFinish { get; private set; }
        public Vector3 Velocity => _velocity;

        public void Initialize(Transform[] path, MovementType type, float maxDistance, float speed)
        {
            _maxDistance = maxDistance;
            _movementType = type;
            _pathPoints = path;
            _speed = speed;

            _target = _pathPoints[1].position;
        }

        public void Move()
        {
            CalculateVelocity();

            if ((transform.position - _target).sqrMagnitude < _maxDistance)
            {
                GetNextPoint();
            }

            Vector3 direction = _target - transform.position;
            transform.forward = Vector3.Lerp(transform.forward, direction, Time.deltaTime * _speedRotation);

            if (_movementType == MovementType.Move)
            {
                transform.Translate(_velocity * Time.deltaTime, Space.World);
            }
            else if (_movementType == MovementType.Lerp)
            {
                transform.position = Vector3.Lerp(transform.position, _target, Time.deltaTime * _speed);
            }
        }

        private void GetNextPoint()
        {
            if (_indexPoint >= _pathPoints.Length - 1)
            {
                IsFinish = true;
                return;
            }

            _indexPoint++;
            _target = _pathPoints[_indexPoint].position;
        }

        private void CalculateVelocity()
        {
            var aim = _target - transform.position;

            if (aim.magnitude > 0.1f)
            {
                _velocity = aim.normalized * _speed;
            }
            else
            {
                _velocity = Vector3.zero;
            }
        }
    }
}