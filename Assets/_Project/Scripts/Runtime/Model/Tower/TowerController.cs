using System;
using UnityEngine;

namespace MicroTowerDefence
{
    public class TowerController : ITowerInteraction, IReset, IUpdate, IPause, IDispose
    {
        public event Action<uint, uint> OnTowerCostEvent;

        private readonly TowerPreview _preview;
        private readonly ProjectileController _projectileController;
        private readonly TowerFactory _towerFactory;
        private readonly Coins _coins;

        private readonly BehaviourCollection _towers = new();

        private bool _isPause;

        public TowerController(TowerPreview preview,
                               TowerFactory towerFactory,
                               ProjectileController projectileController,
                               Coins coins)
        {
            _coins = coins;
            _preview = preview;
            _towerFactory = towerFactory;
            _projectileController = projectileController;

            _preview.OnBuildEvent += OnBuild;
        }

        public void GameUpdate()
        {
            if (_isPause) return;

            _towers.GameUpdate();
        }

        public void Pause(bool isPause)
        {
            _isPause = isPause;
        }

        private void OnBuild(TowerBase tower)
        {
            var cost = _towerFactory.GetCostToBuild(tower.TowerType);

            if (_coins.TrySpend(cost))
            {
                tower.SetProjectile(_projectileController);
                tower.OnInteractEvent += OnTowerCost;
                tower.IsInit = true;
                tower.Undo();
                _towers.Add(tower);
            }
            else
            {
                tower.Reclaim();
            }
        }

        public void OnSell(TowerBase tower)
        {
            var coins = tower.SellCost;
            tower.OnInteractEvent -= OnTowerCost;
            tower.Reclaim();
            _towers.Remove(tower);
            _coins.Add(coins);
        }

        public void OnUpgrade(TowerBase tower)
        {
            if (tower.Level >= tower.MaxLevel)
            {
                Debug.Log("max level");
                return;
            }

            if (_coins.TrySpend(tower.UpgradeCost))
            {
                var newTower = _towerFactory.Get(tower.TowerType, tower.Level + 1);
                var position = tower.Position;
                tower.Reclaim();
                _towers.Remove(tower);

                newTower.SetProjectile(_projectileController);
                newTower.OnInteractEvent += OnTowerCost;
                newTower.Position = position;
                newTower.IsInit = true;
                newTower.Undo();
                _towers.Add(newTower);
            }
        }

        private void OnTowerCost(TowerBase tower)
        {
            OnTowerCostEvent?.Invoke(tower.UpgradeCost, tower.SellCost);
        }

        void IReset.Reset()
        {
            _towers.Clear();
        }

        public void Dispose()
        {
            _preview.OnBuildEvent -= OnBuild;
        }
    }
}