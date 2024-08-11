using System.Threading.Tasks;
using UnityEngine;

namespace MicroTowerDefence
{
    public abstract class AnimationControllerBase : MonoBehaviour
    {
        public bool IsInited { get; protected set; }
        public Animator Animator => _animator;

        protected const string DIE_KEY = "Die";
        protected const string APPEAR_KEY = "Appear";
        protected const string WALK_KEY = "Walk";

        protected Animator _animator;
        protected EnemyBase _enemy;

        private readonly int _delayAnimation = 1000;

        public virtual void Initialize(EnemyBase enemy)
        {
            _animator = GetComponent<Animator>();
            _enemy = enemy;
            Animate();
        }

        private async void Animate()
        {
            _animator.Play(APPEAR_KEY);
            await Task.Delay(_delayAnimation);
            _animator.Play(WALK_KEY);
            GetComponentInParent<TargetPoint>().IsEnabled = true;
            IsInited = true;
        }

        public void SetSpeedFactor(float factor)
        {
            _animator.speed = factor;
        }

        public async void Die()
        {
            _animator.Play(DIE_KEY);
            GetComponentInParent<TargetPoint>().IsEnabled = false;
            await Task.Delay(_delayAnimation);
            _enemy.Reclaim();
        }
    }
}