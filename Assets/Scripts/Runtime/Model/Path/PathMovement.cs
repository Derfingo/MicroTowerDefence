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
            Vector3 direction = _target - transform.position;
            transform.forward = direction;

            if (Vector3.Distance(transform.position, _target) <= _maxDistance)
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

            if (aim.magnitude > 0.5f)
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