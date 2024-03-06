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

        protected override void DetectGround(Collision collision)
        {
            if (collision.gameObject.layer == _groundLayer)
            {
                print(_blastRadious);
                _projectile.GetExplosion().Initialize(transform.position, _blastRadious, _damage);
                Reclaim();
            }
        }

        protected override void DetectEnemy(Collision collision)
        {
            if (collision.gameObject.layer == _enemyLayer)
            {
                _projectile.GetExplosion().Initialize(transform.position, _blastRadious, _damage);
                Reclaim();
            }
        }
    }
}