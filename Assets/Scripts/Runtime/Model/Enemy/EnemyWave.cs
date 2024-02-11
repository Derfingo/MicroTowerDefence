using UnityEngine;

namespace MicroTowerDefence
{
    [CreateAssetMenu]
    public class EnemyWave : ScriptableObject
    {
        [SerializeField] private EnemySpawnSequence[] _spawnSequences;

        public State Begin(EnemyContorller contorller, PathConfig config) => new(this, contorller, config);

        public struct State
        {
            private EnemyContorller _contorller;
            private PathConfig _config;
            private EnemyWave _wave;
            private int _index;
            private EnemySpawnSequence.State _sequence;

            public State(EnemyWave wave, EnemyContorller contorller, PathConfig config)
            {
                _contorller = contorller;
                _config = config;
                _wave = wave;
                _index = 0;
                _sequence = _wave._spawnSequences[0].Begin(contorller, config);
            }

            public float Progress(float deltaTime)
            {
                deltaTime = _sequence.Progress(deltaTime);

                while (deltaTime >= 0f)
                {
                    if (++_index >= _wave._spawnSequences.Length)
                    {
                        return deltaTime;
                    }

                    _sequence = _wave._spawnSequences[_index].Begin(_contorller, _config);
                    deltaTime = _sequence.Progress(deltaTime);
                }

                return -1f;
            }
        }
    }
}