using UnityEngine;

public class UpgradeTower : MonoBehaviour
{
    [SerializeField] private UIController _uiController;
    [SerializeField] private GameBoard _board;

    private void Start()
    {
        _uiController.OnUpgradeClick += OnUpgradeTower;
    }

    private void OnUpgradeTower(GameTile tile)
    {
        // check money

        if (tile.Content.Level == 2)
        {
            Debug.Log("max level");
            return;
        }

        _board.ReplaceTile(tile, tile.Content.Level + 1);
    }
}