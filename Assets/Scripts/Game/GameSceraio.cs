using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameSceraio : ScriptableObject
{
    [SerializeField] private EnemyWave[] _waves;

    public State Begin(EnemyContorller controller) => new(this, controller);

    [Serializable]
    public struct State
    {
        private EnemyContorller _controller;
        private GameSceraio _scenario;
        private int _index;
        private EnemyWave.State _wave;

        public State(GameSceraio scenario, EnemyContorller controller)
        {
            _controller = controller;
            _scenario = scenario;
            _index = 0;
            _wave = scenario._waves[0].Begin(controller);
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

                _wave = _scenario._waves[_index].Begin(_controller);
                deltaTime = _wave.Progress(deltaTime);
            }

            return true;
        }
    }
}
