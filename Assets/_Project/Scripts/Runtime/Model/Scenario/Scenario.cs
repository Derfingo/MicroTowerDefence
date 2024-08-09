using UnityEngine;
using Zenject;

namespace MicroTowerDefence
{
    public class Scenario : MonoBehaviour, IReset
    {
        [SerializeField] private EnemyWavesConfig _config;

        public bool IsEnd { get; private set; }

        private EnemyContorller _enemyContorller;
        private EnemyFactory _factory;

        private Wave[] _waves;

        private Wave _target;
        private int _index;

        [Inject]
        public void Initialize(EnemyFactory factory, EnemyContorller contorller)
        {
            _factory = factory;
            _enemyContorller = contorller;
            _index = 0;

            InitializeWaves();
        }

        public void Begin()
        {
            _target = _waves[_index];
            _target.Begin();
        }

        public void GameUpdate()
        {
            _target.Update();
        }

        private void OnSpawn(EnemyType type)
        {
            _enemyContorller.Spawn(_factory, type);
        }

        private void OnNext()
        {
            _index++;

            if (_index <= _waves.Length - 1)
            {
                _target = _waves[_index];
                _target.Begin();
            }
            else
            {
                IsEnd = true;
            }
        }

        private void InitializeWaves()
        {
            _waves = _config.Waves;

            foreach (var wave in _waves)
            {
                wave.OnSpawnEvent += OnSpawn;
                wave.OnNextEvent += OnNext;
            }
        }

        void IReset.Reset()
        {
            _index = 0;

            foreach (var wave in _waves)
            {
                wave.Reset();
            }
        }
    }
}