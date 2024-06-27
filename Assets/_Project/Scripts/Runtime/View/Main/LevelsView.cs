using UnityEngine;
using UnityEngine.UI;

namespace MicroTowerDefence
{
    public class LevelsView : ViewBase
    {
        [SerializeField] private LevelButton[] _levelButtons;

        public LevelButton[] LevelButtons => _levelButtons;
    }
}