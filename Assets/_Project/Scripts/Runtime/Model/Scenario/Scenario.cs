namespace MicroTowerDefence
{
    public class Scenario : IReset, IUpdate, IPause
    {
        public bool IsEnd { get; private set; }

        private readonly EnemyWavesConfig _wavesConfig;
        private readonly EnemyContorller _enemyContorller;
        private readonly EnemyFactory _factory;

        private Wave[] _waves;
        private Wave _target;
        private bool _isPause;
        private int _index;
        
        public Scenario(EnemyFactory factory, EnemyContorller contorller, EnemyWavesConfig wavesConfig)
        {
            _factory = factory;
            _wavesConfig = wavesConfig;
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
            if (_isPause) return;

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
            _waves = _wavesConfig.Waves;

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

        public void Pause(bool isPause)
        {
            _isPause = isPause;
        }
    }
}