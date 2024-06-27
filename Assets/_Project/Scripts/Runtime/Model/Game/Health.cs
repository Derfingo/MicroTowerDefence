using System;
using UnityEngine;
using Zenject;

namespace MicroTowerDefence
{
    public class Health : MonoBehaviour, IHealth, IReset
    {
        public event Action<uint> UpdateHealthEvent;

        private uint _initialHealth;
        private uint _health;

        [Inject]
        public void Initialize(uint health)
        {
            _initialHealth = health;
            _health = health;
            UpdateHealthEvent?.Invoke(_initialHealth);
        }

        public void Add(uint health)
        {
            _health += health;
            UpdateHealthEvent?.Invoke(_health);
        }

        public bool TryTakeDamage(uint damage)
        {
            if (_health <= damage)
            {
                _health = 0;
                Debug.Log($"health = {_health}");
                UpdateHealthEvent?.Invoke(_health);
                return true;
            }

            _health -= damage;
            UpdateHealthEvent?.Invoke(_health);
            return false;
        }

        void IReset.Reset()
        {
            _health = _initialHealth;
            UpdateHealthEvent?.Invoke(_health);
        }
    }
}