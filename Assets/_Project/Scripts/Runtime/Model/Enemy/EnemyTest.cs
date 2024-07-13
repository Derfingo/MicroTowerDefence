using UnityEngine;

namespace MicroTowerDefence
{
    public class EnemyTest : MonoBehaviour
    {
        [SerializeField] private RaycastController _raycast;

        public Vector3 Velocity;
        public Vector3 Velocity2 => _speed * transform.forward;

        private float _speed = 2f;

        private void Update()
        {
            var target = _raycast.GetPosition();
            target.y = 0.25f;
            CalculateVelocity(target);
            transform.position += Velocity * Time.deltaTime;
        }

        private void CalculateVelocity(Vector3 position)
        {
            var aim = position - transform.position;

            if (aim.magnitude > 0.2f)
            {
                Velocity = aim.normalized * _speed;
            }
            else
            {
                Velocity = Vector3.zero;
            }
        }
    }
}