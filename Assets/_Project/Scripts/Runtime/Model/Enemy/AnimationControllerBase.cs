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

        public virtual void Initialize(EnemyBase enemy)
        {
            _animator = GetComponent<Animator>();
            _enemy = enemy;
            Animate();
        }

        public void SetSpeedFactor(float factor)
        {
            _animator.speed = factor;
        }

        public async void Die()
        {
            _animator.Play(DIE_KEY);
            GetComponentInParent<TargetPoint>().IsEnabled = false;
            await Task.Delay(GetLengthAnimation());
            _enemy.Reclaim();
        }

        private async void Animate()
        {
            _animator.Play(APPEAR_KEY);
            await Task.Delay(GetLengthAnimation());
            _animator.Play(WALK_KEY);
            GetComponentInParent<TargetPoint>().IsEnabled = true;
            IsInited = true;
        }

        private int GetLengthAnimation()
        {
            var clipInfo = _animator.GetCurrentAnimatorClipInfo(0);
            int delay = Mathf.RoundToInt(clipInfo[0].clip.length * 1000);
            return delay;
        }
    }
}