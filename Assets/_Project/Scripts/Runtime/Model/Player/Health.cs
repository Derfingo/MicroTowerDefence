using System;

namespace MicroTowerDefence
{
    public class Health : IHealth, IReset
    {
        public event Action<uint> OnChangeHealthEvent;
        public event Action OnHealthOverEvent;

        private readonly uint _initialHealth;
        private uint _health;

        public Health(uint health)
        {
            _initialHealth = health;
            _health = health;
            OnChangeHealthEvent?.Invoke(_initialHealth);
        }

        public void Add(uint health)
        {
            _health += health;
            OnChangeHealthEvent?.Invoke(_health);
        }

        public void TakeDamage(uint damage)
        {
            _health -= damage;

            if (_health <= damage)
            {
                _health = 0;
                //Debug.Log($"health = {damage}");
                OnChangeHealthEvent?.Invoke(_health);
                OnHealthOverEvent?.Invoke();
            }

            OnChangeHealthEvent?.Invoke(_health);
        }

        void IReset.Reset()
        {
            _health = _initialHealth;
            OnChangeHealthEvent?.Invoke(_health);
        }
    }
}