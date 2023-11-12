using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTower : MonoBehaviour
{
    [SerializeField] private UIController _uiController;

    private void Start()
    {
        _uiController.OnUpgradeClick += OnUpgradeTower;
    }

    private void OnUpgradeTower(GameTile tile)
    {
        Tower tower = tile.Content.GetComponent<Tower>();

        // check money
        if (tower.CurrentLevel == TowerLevel.Third)
        {
            Debug.Log("max level");
            return;
        }

        tower.SetLevel(tower.CurrentLevel + 1);
    }
}
