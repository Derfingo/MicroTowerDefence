using UnityEngine;

public class BuildingController : MonoBehaviour
{
    [SerializeField] private ContentSelector _contentSelector;
    [SerializeField] private Coins _coins;

    private TileContentFactory _contentFactory;
    private TileContent _tempContent;

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

    private void OnBuild(TileContent content, Vector3 position)
    {
        _tempContent = content;
        var tower = content.GetComponent<TowerBase>();

        if (_coins.TrySpend(tower.Cost))
        {
            content.transform.position = position;
        }
        else
        {
            content.Recycle();
        }
    }

    private void OnSell()
    {
        _tempContent.Recycle();
        _coins.Add(30);
        _tempContent = null;
    }

    private void OnUpgrade()
    {
        var tower = _tempContent.GetComponent<TowerBase>();
        var level = _tempContent.Level;
        var type = tower.TowerType;
        var position = _tempContent.transform.position;

        if (level == 2)
        {
            Debug.Log("max level");
            return;
        }

        if (_coins.TrySpend(tower.Cost))
        {
            _tempContent.Recycle();
            var content = _contentFactory.Get(type, level + 1);
            content.transform.position = position;
            _tempContent = content;
        }
    }
}