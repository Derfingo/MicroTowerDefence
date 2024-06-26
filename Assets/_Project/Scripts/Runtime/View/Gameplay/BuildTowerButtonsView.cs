using System.Collections.Generic;
using UnityEngine;

namespace MicroTowerDefence
{
    public class BuildTowerButtonsView : ViewBase
    {
        [SerializeField] private List<BuildTowerButton> _towerButtons;

        public List<BuildTowerButton> TowerButtons => _towerButtons;
    }
}