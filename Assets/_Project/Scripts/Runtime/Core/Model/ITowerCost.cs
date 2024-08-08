using System.Collections.Generic;

namespace MicroTowerDefence
{
    public interface ITowerCost
    {
        Dictionary<TowerType, uint> GetAllCostTowers();
    }
}
