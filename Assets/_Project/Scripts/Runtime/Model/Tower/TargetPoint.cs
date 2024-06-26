using UnityEngine;

namespace MicroTowerDefence
{
    [RequireComponent(typeof(SphereCollider))]
    public class TargetPoint : MonoBehaviour
    {
        public Vector3 Position => transform.position;
        public Vector3 Velocity => Enemy.Velocity;
        public EnemyBase Enemy { get; private set; }
        public float ColliderSize { get; private set; }
        public static int BufferedCount { get; private set; }

        private bool _isEnabled;
        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                _collider.enabled = value;
                _isEnabled = value;
            }
        }

        private SphereCollider _collider;
        private const int ENEMY_LAYER_MASK = 1 << 9;
        private static readonly Collider[] _buffer = new Collider[100];

        private void Awake()
        {
            Enemy = transform.root.GetComponent<EnemyBase>();
            _collider = GetComponent<SphereCollider>();
            ColliderSize = GetComponent<SphereCollider>().radius * transform.localScale.x;
        }

        public static bool FillBuffer(Vector3 position, float range)
        {
            Vector3 top = position;
            top.y += 3f;
            BufferedCount = Physics.OverlapCapsuleNonAlloc(position, top, range, _buffer, ENEMY_LAYER_MASK);
            return BufferedCount > 0;
        }

        public static TargetPoint GetBuffered(int index)
        {
            var target = _buffer[index].GetComponent<TargetPoint>();
            return target;
        }
    }
}