using UnityEngine;

public abstract class EnemyView : MonoBehaviour
{
    public bool IsInited {  get; protected set; }

    protected const string DIED_KEY = "Died";
    protected Animator _animator;
    protected Enemy _enemy;

    public virtual void Initialize(Enemy enemy)
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
        GetComponent<TargetPoint>().IsEnabled = true;
    }
}
