using System;
using UnityEngine;

namespace MicroTowerDefence
{
    public class ProjectileController : IReset, IUpdate, IPause, IDisposable
    {
        private readonly ProjectileFactory _factory;
        private readonly BehaviourCollection _projectiles = new();

        private bool _isPause;

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
            if (_isPause) return;

            _projectiles.GameUpdate();
        }

        public void Pause(bool isPause)
        {
            _isPause = isPause;
        }

        void IReset.Reset()
        {
            _projectiles.Clear();
        }

        public void Dispose()
        {
            _projectiles.Clear();
        }
    }
}