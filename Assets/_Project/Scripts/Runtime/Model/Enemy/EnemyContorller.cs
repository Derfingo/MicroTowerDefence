using UnityEngine;

namespace MicroTowerDefence
{
    public class EnemyContorller : IUpdate, IReset, IPause
    {
        public bool IsEmpty => _enemies.IsEmpty;

        private readonly GameBehaviourCollection _enemies = new();
        private readonly Health _health;
        private readonly Coins _coins;

        private PathConfig _pathConfig;

        public EnemyContorller(Health health, Coins coins, PathConfig pathConfig)
        {
            _pathConfig = pathConfig;
            _health = health;
            _coins = coins;
        }

        public void Spawn(EnemyFactory factory, EnemyType type)
        {
            EnemyBase enemy = factory.Get(type);
            enemy.SetPath(_pathConfig);
            enemy.SetPosition(_pathConfig.InitialPoint);
            enemy.OnFinish += _health.TakeDamage;
            enemy.OnDie += _coins.Add;
            _enemies.Add(enemy);
        }

        public void Pause(bool isPause)
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