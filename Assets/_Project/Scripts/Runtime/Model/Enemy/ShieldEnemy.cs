using UnityEngine;

namespace MicroTowerDefence
{
    public class ShieldEnemy : EnemyBase
    {
        [SerializeField] private ShieldView _shield;

        public override void Initialize(EnemyConfig config)
        {
            base.Initialize(config);
            _shield.Initialize(config.Shield.RandomValueInRange);
        }
    }
}