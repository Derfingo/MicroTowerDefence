using System;
using UnityEngine;

namespace MicroTowerDefence
{
    public class TowerController : ITowerController, IUpdate, IReset
    {
        public event Action<uint, uint> OnTowerCostEvent;

        private readonly TowerPreview _preview;
        private readonly ProjectileController _projectileController;
        private readonly TowerFactory _towerFactory;
        private readonly Coins _coins;

        private readonly GameBehaviourCollection _towers = new();

        public TowerController(TowerPreview preview,
                               TowerFactory contentFactory,
                               ProjectileController projectileController,
                               Coins coins)
        {
            _coins = coins;
            _preview = preview;
            _towerFactory = contentFactory;
            _projectileController = projectileController;

            _preview.OnBuildEvent += OnBuild;
        }

        public void GameUpdate()
        {
            _towers.GameUpdate();
        }

        void IReset.Reset()
        {
            _towers.Clear();
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

        public void OnSell(TileContent content, uint coins)
        {
            var tower = content.GetComponent<TowerBase>();
            tower.OnInteractEvent -= OnTowerCost;
            tower.Reclaim();
            _towers.Remove(tower);
            _coins.Add(coins);
        }

        public void OnUpgrade(TileContent content)
        {
            var tower = content.GetComponent<TowerBase>();

            if (tower.Level >= tower.MaxLevel)
            {
                Debug.Log("max level");
                return;
            }

            if (_coins.TrySpend(tower.UpgradeCost))
            {
                var newTower = _towerFactory.Get(tower.TowerType, tower.Level + 1);
                var position = tower.Position;
                _towers.Remove(tower);
                tower.Reclaim();

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

        ~TowerController()
        {
            _preview.OnBuildEvent -= OnBuild;
        }
    }
}