using UnityEngine;

namespace MicroTowerDefence
{
    [CreateAssetMenu]
    public class EnemyWave : ScriptableObject
    {
        [SerializeField] private EnemySpawnSequence[] _spawnSequences;

        public State Begin(EnemyContorller contorller) => new(this, contorller);

        public struct State
        {
            private EnemyContorller _contorller;
            private EnemyWave _wave;
            private int _index;
            private EnemySpawnSequence.State _sequence;

            public State(EnemyWave wave, EnemyContorller contorller)
            {
                _contorller = contorller;
                _wave = wave;
                _index = 0;
                _sequence = _wave._spawnSequences[0].Begin(contorller);
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

                    _sequence = _wave._spawnSequences[_index].Begin(_contorller);
                    deltaTime = _sequence.Progress(deltaTime);
                }

                return -1f;
            }
        }
    }
}