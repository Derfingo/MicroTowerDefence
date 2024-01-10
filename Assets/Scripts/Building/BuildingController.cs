using UnityEngine;

public class BuildingController : MonoBehaviour
{
    [SerializeField] private ProjectileController _projectileController;
    [SerializeField] private ContentSelector _contentSelector;
    [SerializeField] private Coins _coins;

    private TileContentFactory _contentFactory;
    private readonly GameBehaviourCollection _buildings = new();

    private void Start()
    {
        _contentSelector.OnBuild += OnBuild;
        _contentSelector.OnSell += OnSell;
        _contentSelector.OnUpdate += OnUpgrade;
    }

    public void Initialize(TileContentFactory contentFactory)
    {
        _contentFactory = contentFactory;
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
            content.Position = position;
            _buildings.Add(content);
        }
        else
        {
            content.Recycle();
        }
    }

    private void OnSell(TileContent content)
    {
        _buildings.Remove(content);
        content.Recycle();
        _coins.Add(30);
    }

    private void OnUpgrade(TileContent content)
    {
        if (content.Level == 2)
        {
            Debug.Log("max level");
            return;
        }

        var tower = content.GetComponent<TowerBase>();
        var newTower = _contentFactory.Get(tower.TowerType, tower.Level + 1) as TowerBase; 
        var cost = newTower.Cost;

        if (_coins.TrySpend(cost))
        {
            var position = content.Position;
            _buildings.Remove(content);
            content.Recycle();
            
            newTower.SetProjectile(_projectileController);
            newTower.Position = position;
            _buildings.Add(newTower);
        }
        else
        {
            newTower.Recycle();
        }
    }
}