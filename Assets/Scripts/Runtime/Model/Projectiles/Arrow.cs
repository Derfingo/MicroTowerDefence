using UnityEngine;

namespace MicroTowerDefence
{
    public class Arrow : ProjectileBase
    {
        private float _reclaimDelay = 1f;

        public override bool GameUpdate()
        {
            if(_isMoving == false)
            {
                return false;
            }

            Move();

            return true;
        }

        protected override void Move()
        {
            if(_isMoving)
            {
                transform.forward = _rigidbBody.velocity;
            }
        }

        protected override void DetectGround(Collision collision)
        {
            if (collision.gameObject.layer == _groundLayer)
            {
                Reclaim(_reclaimDelay);
            }
        }

        protected override void DetectEnemy(Collision collision)
        {
            if (collision.gameObject.layer == _enemyLayer)
            {
                if (collision.gameObject.TryGetComponent(out Enemy enemy))
                {
                    enemy.TakeDamage(_damage);
                    Reclaim();
                }
            }
        }
    }
}