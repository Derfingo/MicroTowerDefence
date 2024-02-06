using UnityEngine;

public class TowerController : MonoBehaviour, ITowerControllerModel
{
    [SerializeField] private ProjectileController _projectileController;
    [SerializeField] private Coins _coins;

    private TowerFactory _towerFactory;
    private readonly GameBehaviourCollection _towers = new();

    public void Initialize(TowerFactory contentFactory)
    {
        _towerFactory = contentFactory;
    }

    public void GameUpdate()
    {
        _towers.GameUpdate();
    }

    public void Clear()
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
            tower.Position = position;
            tower.IsInit = true;
            _towers.Add(tower);
        }
        else
        {
            tower.Destroy();
        }
    }

    public void OnSell(TileContent content, uint coins)
    {
        var tower = content.GetComponent<TowerBase>();
        _towers.Remove(tower);
        tower.Destroy();
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
            tower.Destroy();
            
            newTower.SetProjectile(_projectileController);
            newTower.Position = position;
            newTower.IsInit = true;
            _towers.Add(newTower);
        }
    }
}