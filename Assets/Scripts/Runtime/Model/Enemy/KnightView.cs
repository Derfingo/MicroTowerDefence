namespace MicroTowerDefence
{
    public class KnightView : EnemyView
    {
        public void OnDieAnimationFinished()
        {
            _enemy.Destroy();
        }
    }
}