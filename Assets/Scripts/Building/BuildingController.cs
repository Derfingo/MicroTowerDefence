using UnityEngine;

public class BuildingController : MonoBehaviour
{
    [SerializeField] private ProjectileController _projectileController;
    [SerializeField] private ContentSelector _contentSelector;
    [SerializeField] private Coins _coins;

    private TowerFactory _towerFactory;
    private readonly GameBehaviourCollection _buildings = new();

    private void Start()
    {
        _contentSelector.OnBuild += OnBuild;
        _contentSelector.OnSell += OnSell;
        _contentSelector.OnUpdate += OnUpgrade;
    }

    public void Initialize(TowerFactory contentFactory)
    {
        _towerFactory = contentFactory;
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
        tower.SetProjectile(_projectileController);

        if (_coins.TrySpend(tower.Cost))
        {
            tower.Position = position;
            tower.IsInit = true;
            _buildings.Add(content);
        }
        else
        {
            content.Destroy();
        }
    }

    private void OnSell(TileContent content)
    {
        _buildings.Remove(content);
        content.Destroy();
        _coins.Add(30);
    }

    private void OnUpgrade(TileContent content)
    {
        var tower = content.GetComponent<TowerBase>();

        if (tower.Level == 2)
        {
            Debug.Log("max level");
            return;
        }

        var newTower = _towerFactory.Get(tower.TowerType, tower.Level + 1); 
        var cost = newTower.Cost;

        if (_coins.TrySpend(cost))
        {
            var position = content.Position;
            _buildings.Remove(content);
            content.Destroy();
            
            newTower.SetProjectile(_projectileController);
            newTower.Position = position;
            newTower.IsInit = true;
            _buildings.Add(newTower);
        }
        else
        {
            newTower.Destroy();
        }
    }
}