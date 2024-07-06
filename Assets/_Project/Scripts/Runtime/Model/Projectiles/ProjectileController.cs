namespace MicroTowerDefence
{
    public class ProjectileController : IUpdate, IReset
    {
        private readonly ProjectileFactory _factory;

        private readonly GameBehaviourCollection _projectiles = new();
        
        public ProjectileController(ProjectileFactory factory)
        {
            _factory = factory;
        }

        public ProjectileBase Get(ProjectileType type, uint level)
        {
            var projectile = _factory.Get(type, level);
            _projectiles.Add(projectile);
            return projectile;
        }

        public void GameUpdate()
        {
            _projectiles.GameUpdate();
        }

        void IReset.Reset()
        {
            _projectiles.Clear();
        }
    }
}