using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField] private ProjectileController _projectileController;
    [SerializeField] private ContentSelectionView _contentSelector;
    [SerializeField] private Coins _coins;

    private TowerFactory _towerFactory;
    private readonly GameBehaviourCollection _buildings = new();

    public void Initialize(TowerFactory contentFactory)
    {
        _towerFactory = contentFactory;

        _contentSelector.BuildEvent += OnBuild;
        _contentSelector.SellEvent += OnSell;
        _contentSelector.UpgradeEvent += OnUpgrade;
    }

    public void GameUpdate()
    {
        _buildings.GameUpdate();
    }

    public void Clear()
    {
        _buildings.Clear();
    }

    private void OnBuild(TileContent content, Vector3 position)
    {
        var tower = content.GetComponent<TowerBase>();
        var cost = _towerFactory.GetCost(tower.TowerType);

        if (_coins.TrySpend(cost))
        {
            tower.SetProjectile(_projectileController);
            tower.Position = position;
            tower.IsInit = true;
            _buildings.Add(content);
        }
        else
        {
            content.Destroy();
        }
    }

    private void OnSell(TileContent content, uint coins)
    {
        _buildings.Remove(content);
        content.Destroy();
        _coins.Add(coins);
    }

    private void OnUpgrade(TileContent content)
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
            var position = content.Position;
            _buildings.Remove(content);
            content.Destroy();
            
            newTower.SetProjectile(_projectileController);
            newTower.Position = position;
            newTower.IsInit = true;
            _buildings.Add(newTower);
        }
    }
}