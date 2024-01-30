using UnityEngine;
using UnityEngine.UI;

public class LevelsView : ViewBase
{
    [SerializeField] private Button[] _levelButtons;

    public Button[] LevelButtons => _levelButtons;


}
