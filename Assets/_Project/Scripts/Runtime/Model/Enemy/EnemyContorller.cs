namespace MicroTowerDefence
{
    public class EnemyContorller : IReset, IUpdate, IPause
    {
        public bool IsEmpty => _enemies.IsEmpty;

        private readonly BehaviourCollection _enemies = new();
        private readonly PathConfig _pathConfig;
        private readonly Health _health;
        private readonly Coins _coins;
        private bool _isPause;

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

        public void GameUpdate()
        {
            if (_isPause) return;

            _enemies.GameUpdate();
        }

        public void Pause(bool isPause)
        {
            _isPause = isPause;

            if (IsEmpty == false)
            {
                foreach (var behaviour in _enemies.Behaviours)
                {
                    behaviour.GetComponent<EnemyBase>().SetPause(!isPause);
                }
            }
        }

        void IReset.Reset()
        {
            _enemies.Clear();
        }
    }
}