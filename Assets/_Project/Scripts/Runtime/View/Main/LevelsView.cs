using UnityEngine;
using UnityEngine.UI;

namespace MicroTowerDefence
{
    public class LevelsView : ViewBase
    {
        [SerializeField] private Button[] _levelButtons;

        public Button[] LevelButtons => _levelButtons;


    }
}