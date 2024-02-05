using System;
using UnityEngine;

public class Health : MonoBehaviour, IHealthModel
{
    //[SerializeField] private HealthView _view;
    public event Action<uint> UpdateHealthEvent;

    private uint _initialHealth;
    private uint _health;


    public void Initialize(uint health)
    {
        _initialHealth = health;
        _health = health;
        //_view.UpdateHealth(_health);
        UpdateHealthEvent?.Invoke(_initialHealth);
    }

    public void Add(uint health)
    {
        _health += health;
        //_view.UpdateHealth(_health);
        UpdateHealthEvent?.Invoke(_health);
    }

    public bool TryTakeDamage(uint damage)
    {
        if (_health <= damage)
        {
            _health = 0;
            Debug.Log($"health = {_health}");
            //_view.UpdateHealth(_health);
            UpdateHealthEvent?.Invoke( _health);
            return true;
        }

        _health -= damage;
        //_view.UpdateHealth(_health);
        UpdateHealthEvent?.Invoke(_health);
        return false;
    }

    public void Reset()
    {
        _health = _initialHealth;
        //_view.UpdateHealth(_health);
        UpdateHealthEvent?.Invoke(_health);
    }
}
