using System;
using UnityEngine;

namespace MicroTowerDefence
{
    public class EnemyContorller : MonoBehaviour, IUpdate, IReset
    {
        public event Action<uint> EnemyFinishEvent;
        public event Action<uint> EnemyDieEvent;

        private readonly GameBehaviourCollection _enemies = new();

        public bool IsEmpty => _enemies.IsEmpty;

        public void Spawn(EnemyFactory factory, EnemyType type, PathConfig config)
        {
            Enemy enemy = factory.Get(type);
            enemy.SetPath(config);
            enemy.SetPosition(config.InitialPoint);
            enemy.OnFinish += EnemyFinishEvent;
            enemy.OnDie += EnemyDieEvent;
            _enemies.Add(enemy);
        }

        public void SetPause(bool isPause)
        {
            foreach (var enemy in _enemies.Behaviours)
            {
                enemy.GetComponentInChildren<Animator>().enabled = !isPause;
            }
        }

        void IReset.Reset()
        {
            _enemies.Clear();
        }

        public void GameUpdate()
        {
            _enemies.GameUpdate();
        }
    }
}