using UnityEngine;

namespace MicroTowerDefence
{
    public class Arrow : ProjectileBase
    {
        private readonly float _reclaimDelay = 1f;

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

        protected override void Collide(Collision collision, int layerIndex)
        {
            if (layerIndex == _groundLayer)
            {
                Reclaim(_reclaimDelay);
            }

            if (layerIndex == _shieldLayer)
            {
                if (collision.gameObject.TryGetComponent(out IDamage enemy))
                {
                    enemy.TakeDamage(_damage, _elementType);
                    Reclaim(_reclaimDelay);
                }
            }

            if (layerIndex == _enemyLayer)
            {
                if (collision.gameObject.TryGetComponent(out IDamage enemy))
                {
                    enemy.TakeDamage(_damage, _elementType);
                    Reclaim();
                }
            }
        }
    }
}