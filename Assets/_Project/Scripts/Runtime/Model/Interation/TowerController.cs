using System;
using UnityEngine;
using Zenject;

namespace MicroTowerDefence
{
    public class TowerController : MonoBehaviour, ITowerController, IUpdate, IReset
    {
        [SerializeField] private ProjectileController _projectileController;
        [SerializeField] private ContentSelection _contentSelection;
        [SerializeField] private Coins _coins;

        public event Action<TileContent> TowerSelectedEvent;

        private TowerFactory _towerFactory;
        private readonly GameBehaviourCollection _towers = new();

        [Inject]
        public void Initialize(TowerFactory contentFactory)
        {
            _towerFactory = contentFactory;

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
    }
}