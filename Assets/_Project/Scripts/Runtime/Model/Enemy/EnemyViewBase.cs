using UnityEngine;

namespace MicroTowerDefence
{
    public abstract class EnemyViewBase : MonoBehaviour
    {
        public bool IsInited { get; protected set; }

        protected const string DIED_KEY = "Died";
        protected Animator _animator;
        protected EnemyBase _enemy;

        public virtual void Initialize(EnemyBase enemy)
        {
            _animator = GetComponent<Animator>();
            _enemy = enemy;
        }

        public void SetSpeedFactor(float factor)
        {
            _animator.speed = factor;
        }

        public virtual void Die()
        {
            _animator.SetBool(DIED_KEY, true);
        }

        public void OnSpawnAnimationFinished()
        {
            IsInited = true;
            GetComponentInParent<TargetPoint>().IsEnabled = true;
        }
    }
}