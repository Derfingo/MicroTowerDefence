using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyView : MonoBehaviour
{
    public const string DIED_KEY = "Died";

    protected Animator _animator;
    protected Enemy _enemy;

    public virtual void Initialize(Enemy enemy)
    {
        _animator = GetComponent<Animator>();
        _enemy = enemy;
    }

    public virtual void Die()
    {
        _animator.SetBool(DIED_KEY, true);
    }
}
