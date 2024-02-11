using System;
using UnityEngine;

namespace MicroTowerDefence
{
    [CreateAssetMenu]
    public class GameScenario : ScriptableObject
    {
        [SerializeField] private EnemyWave[] _waves;

        public State Begin(EnemyContorller controller, PathConfig config) => new(this, controller, config);

        [Serializable]
        public struct State
        {
            private EnemyContorller _controller;
            private GameScenario _scenario;
            private PathConfig _config;
            private int _index;
            private EnemyWave.State _wave;

            public State(GameScenario scenario, EnemyContorller controller, PathConfig config)
            {
                _controller = controller;
                _scenario = scenario;
                _config = config;
                _index = 0;
                _wave = scenario._waves[0].Begin(controller, config);
            }

            public bool Progress()
            {
                float deltaTime = _wave.Progress(Time.deltaTime);

                while (deltaTime >= 0f)
                {
                    if (++_index >= _scenario._waves.Length)
                    {
                        return false;
                    }

                    _wave = _scenario._waves[_index].Begin(_controller, _config);
                    deltaTime = _wave.Progress(deltaTime);
                }

                return true;
            }
        }
    }
}