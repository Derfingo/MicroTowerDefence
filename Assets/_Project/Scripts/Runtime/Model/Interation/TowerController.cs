using System;
using UnityEngine;

namespace MicroTowerDefence
{
    public class TowerController : ITowerController, IUpdate, IReset
    {
        public event Action<TileContent> TowerSelectedEvent;

        private readonly ProjectileController _projectileController;
        private readonly ContentSelection _contentSelection;
        private readonly TowerFactory _towerFactory;
        private readonly Coins _coins;

        private readonly GameBehaviourCollection _towers = new();

        public TowerController(TowerFactory contentFactory,
                               ProjectileController projectileController,
                               ContentSelection contentSelection,
                               Coins coins)
        {
            _towerFactory = contentFactory;
            _projectileController = projectileController;
            _contentSelection = contentSelection;
            _coins = coins;

            _contentSelection.UpgradeEvent += OnUpgrade;
            _contentSelection.BuildEvent += OnBuild;
            _contentSelection.SellEvent += OnSell;
        }

        public void GameUpdate()
        {
            _towers.GameUpdate();
        }

        void IReset.Reset()
        {
            _towers.Clear();
        }

        public void OnBuild(TileContent content, Vector3 position)
        {
            var tower = content.GetComponent<TowerBase>();
            var cost = _towerFactory.GetCostToBuild(tower.TowerType);

            if (_coins.TrySpend(cost))
            {
                tower.SetProjectile(_projectileController);
                tower.SelectedEvent += TowerSelectedEvent;
                tower.Undo();
                tower.Position = position;
                tower.IsInit = true;
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
            tower.SelectedEvent -= TowerSelectedEvent;
            _towers.Remove(tower);
            tower.Reclaim();
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
                newTower.SelectedEvent += TowerSelectedEvent;
                newTower.Position = position;
                newTower.IsInit = true;
                newTower.Undo();
                _towers.Add(newTower);
            }
        }

        ~TowerController()
        {
            _contentSelection.UpgradeEvent -= OnUpgrade;
            _contentSelection.BuildEvent -= OnBuild;
            _contentSelection.SellEvent -= OnSell;
        }
    }
}