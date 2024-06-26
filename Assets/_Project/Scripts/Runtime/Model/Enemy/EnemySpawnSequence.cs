using System;
using UnityEngine;

namespace MicroTowerDefence
{
    [Serializable]
    public class EnemySpawnSequence
    {
        [SerializeField] private EnemyFactory _factory;
        [SerializeField] private EnemyType _type = EnemyType.MediumSlime;
        [SerializeField, Range(1, 100)] private int _amount = 1;
        [SerializeField, Range(0.1f, 10f)] private float _cooldown = 1f;

        public State Begin(EnemyContorller contorller, PathConfig config) => new(this, contorller, config);

        [Serializable]
        public struct State
        {
            private EnemySpawnSequence _sequence;
            private EnemyContorller _contorller;
            private PathConfig _config;
            private int _count;
            private float _cooldown;

            public State(EnemySpawnSequence sequence, EnemyContorller contorller, PathConfig config)
            {
                _contorller = contorller;
                _config = config;
                _sequence = sequence;
                _count = 0;
                _cooldown = sequence._cooldown;
            }

            public float Progress(float deltaTime)
            {
                _cooldown += deltaTime;

                while (_cooldown >= _sequence._cooldown)
                {
                    _cooldown -= _sequence._cooldown;

                    if (_count >= _sequence._amount)
                    {
                        return _cooldown;
                    }

                    _count++;

                    _contorller.Spawn(_sequence._factory, _sequence._type, _config);
                }

                return -1f;
            }
        }
    }
}