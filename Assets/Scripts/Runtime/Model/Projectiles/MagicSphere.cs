using UnityEngine;

namespace MicroTowerDefence
{
    public class MagicSphere : ProjectileBase
    {
        public override bool GameUpdate()
        {
            if (_isMoving == false)
            {
                return false;
            }

            Move();

            return true;
        }

        protected override void Move()
        {
            transform.forward = _rigidbBody.velocity;
        }

        protected override void DetectGround(Collision collision)
        {
            if (collision.gameObject.layer == _groundLayer)
            {
                Reclaim();
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