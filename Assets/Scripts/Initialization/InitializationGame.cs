using UnityEngine;

public class InitializationGame : MonoBehaviour
{
    [SerializeField] private TowerFactory _towerFactory;
    [SerializeField] private BuildingController _buildingController;
    [SerializeField] private ContentSelector _contentSelector;

    private void Start()
    {
        _buildingController.Initialize(_towerFactory);
        _contentSelector.Initialize(_towerFactory);
    }
}
