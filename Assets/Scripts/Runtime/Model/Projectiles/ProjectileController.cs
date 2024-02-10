using UnityEngine;

namespace MicroTowerDefence
{
    public class ProjectileController : MonoBehaviour, IUpdate, IReset
    {
        [SerializeField] private ProjectileFactory _factory;

        private readonly GameBehaviourCollection _projectiles = new();

        public Shell GetShell()
        {
            var shell = _factory.Shell;
            _projectiles.Add(shell);
            return shell;
        }

        public Explosion GetExplosion()
        {
            var explosion = _factory.Explosion;
            _projectiles.Add(explosion);
            return explosion;
        }

        public Arrow GetArrow()
        {
            var arrow = _factory.Arrow;
            _projectiles.Add(arrow);
            return arrow;
        }

        public MagicSphere GetSphere(uint level)
        {
            var projectile = _factory.Get(ProjectileType.Sphere, level);
            var magicSphere = projectile.GetComponent<MagicSphere>();
            _projectiles.Add(magicSphere);
            return magicSphere;
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