using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    [SerializeField] private ContentSelector _contentSelector;
    [SerializeField] private Coins _coins;

    private TileContentFactory _contentFactory;
    private List<TileContent> _contents = new List<TileContent>();

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

    private void Update()
    {
        for (int i = 0; i < _contents.Count; i++)
        {
            _contents[i].GameUpdate();
        }
    }

    private void OnBuild(TileContent content, Vector3 position)
    {
        var tower = content.GetComponent<TowerBase>();

        if (_coins.TrySpend(tower.Cost))
        {
            content.Position = position;
            _contents.Add(content);
        }
        else
        {
            content.Recycle();
        }
    }

    private void OnSell(TileContent content)
    {
        _contents.Remove(content);
        content.Recycle();
        _coins.Add(30);
    }

    private void OnUpgrade(TileContent content)
    {
        var tower = content.GetComponent<TowerBase>();
        var position = content.Position;
        var level = content.Level;
        var type = tower.TowerType;

        if (level == 2)
        {
            Debug.Log("max level");
            return;
        }

        if (_coins.TrySpend(tower.Cost))
        {
            _contents.Remove(content);
            content.Recycle();
            var newContent = _contentFactory.Get(type, level + 1);
            newContent.Position = position;
            _contents.Add(newContent);
        }
    }
}