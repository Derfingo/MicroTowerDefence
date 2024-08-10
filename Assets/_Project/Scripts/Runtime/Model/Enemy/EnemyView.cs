using UnityEngine;

namespace MicroTowerDefence
{
    public class EnemyView : EnemyViewBase
    {
        public void OnDieAnimationFinished()
        {
            _enemy.Reclaim();
        }
    }
}