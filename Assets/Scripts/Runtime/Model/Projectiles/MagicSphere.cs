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

        protected override void Collide(Collision collision, int layerIndex)
        {
            if (layerIndex == _groundLayer)
            {
                Reclaim();
            }

            if (layerIndex == _enemyLayer)
            {
                if (collision.gameObject.TryGetComponent(out IDamage enemy))
                {
                    enemy.TakeDamage(_damage);
                    Reclaim();
                }
            }
        }
    }
}