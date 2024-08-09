using System;
using UnityEngine;

namespace MicroTowerDefence
{
    [Serializable]
    public class Wave
    {
        [SerializeField, Range(1f, 10f)] private float _cooldown;
        [SerializeField, Range(0, 100)] private int _amount;
        [SerializeField] private EnemyType _type;

        public event Action<EnemyType> OnSpawnEvent;
        public event Action OnNextEvent;

        private int _currentAmount;
        private bool _isProcess;
        private float _time;

        public void Begin()
        {
            _currentAmount = _amount;
            _isProcess = true;
        }

        public void Update()
        {
            if (_isProcess)
            {
                _time += 1 * Time.deltaTime;

                if (_time >= _cooldown)
                {
                    if (_currentAmount >= 1)
                    {
                        _currentAmount--;
                        _time = 0;
                        OnSpawnEvent?.Invoke(_type);
                    }
                    else
                    {
                        OnNextEvent?.Invoke();
                        _isProcess = false;
                    }
                }
            }
        }

        public void Reset()
        {
            _currentAmount = _amount;
        }
    }
}