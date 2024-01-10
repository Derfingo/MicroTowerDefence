using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private uint _initialHealth;
    [SerializeField] private HealthView _view;

    private uint _health;
    public uint Amount => _health;

    private void Start()
    {
        _health = _initialHealth;
        _view.SetHealth(_health);
    }

    public void Add(uint health)
    {
        _health += health;
        _view.SetHealth(_health);
    }

    public bool TryTakeDamage(uint damage)
    {
        if (_health <= damage)
        {
            Debug.Log("health = 0");
            _view.SetHealth(0);
            return true;
        }

        _health -= damage;
        _view.SetHealth(_health);
        return false;
    }

    public void Reset()
    {
        _health = _initialHealth;
        _view.SetHealth(_health);
    }
}
