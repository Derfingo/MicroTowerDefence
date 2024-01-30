using System.Collections.Generic;
using UnityEngine;

public class BuildingView : ViewBase
{
    [SerializeField] private List<BuildTowerButton> _buttons;

    public List<BuildTowerButton> Buttons => _buttons;
}
