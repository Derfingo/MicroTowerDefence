using System.Collections.Generic;
using UnityEngine;

public class BuildingView : ViewBase
{
    [SerializeField] private List<BuildTowerButton> _towerButtons;

    public List<BuildTowerButton> TowerButtons => _towerButtons;
}
