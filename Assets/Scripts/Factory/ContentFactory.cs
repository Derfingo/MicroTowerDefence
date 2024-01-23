using UnityEngine;

[CreateAssetMenu]
public class ContentFactory : GameObjectFactory
{
    private TowerFactory _towerFactory;
    private TrapFactory _trapFactory;

    public TowerBase Get(TowerType type, uint level = 0)
    {
        return _towerFactory.Get(type, level);
    }
}
