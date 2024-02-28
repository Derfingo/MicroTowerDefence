using UnityEngine;

namespace MicroTowerDefence
{
    public class Arrow : ProjectileBase
    {
        public override bool GameUpdate()
        {
            if (DetectCollision())
            {
                Destroy();
                return false;
            }

            if (DetectGround())
            {
                Destroy();
                return false;
            }

            Move();

            return true;
        }

        protected override void Move()
        {
            transform.forward = _rigidbBody.velocity;
        }

        protected override void Rotate()
        {
            Vector3 relativePosition = _middlePoint - transform.position;
            if (relativePosition == Vector3.zero)
            {
                return;
            }
            Quaternion rotation = Quaternion.LookRotation(relativePosition, Vector3.up);
            transform.rotation = rotation;
        }

        private void MoveByBezier()
        {
            float delta = _speed * Time.deltaTime;

            _middlePoint = Vector3.MoveTowards(_middlePoint, _targetPosition, delta);
            transform.position = Vector3.MoveTowards(transform.position, _middlePoint, delta);
        }

        private bool DetectCollision()
        {
            if (TargetPoint.FillBuffer(transform.position, _blastRadious))
            {
                TargetPoint.GetBuffered(0).Enemy.TakeDamage(_damage);
                return true;
            }

            return false;
        }

        private bool DetectGround()
        {
            if (transform.position.y <= 0.1f)
            {
                return true;
            }

            return false;
        }
    }
}