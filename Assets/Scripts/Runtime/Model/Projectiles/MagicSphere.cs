using UnityEngine;

namespace MicroTowerDefence
{
    public class MagicSphere : ProjectileBase
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