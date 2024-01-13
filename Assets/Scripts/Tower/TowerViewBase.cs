using UnityEngine;

public abstract class TowerViewBase : MonoBehaviour
{
    protected Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        Animate();
    }

    protected abstract void Animate();
}
