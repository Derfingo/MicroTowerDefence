using UnityEngine;

namespace MicroTowerDefence
{
    public class Shell : ProjectileBase
    {
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
            transform.forward = _rigidbBody.velocity;
        }

        protected override void Collide(Collision collision, int layerIndex)
        {
            if (layerIndex == _groundLayer || layerIndex == _enemyLayer)
            {
                _projectile.GetExplosion().Initialize(transform.position, _blastRadious, _damage);
                Reclaim();
            }
        }
    }
}