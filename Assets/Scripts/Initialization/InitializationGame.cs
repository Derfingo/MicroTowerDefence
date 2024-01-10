using UnityEngine;

public class InitializationGame : MonoBehaviour
{
    [SerializeField] private TileContentFactory _contentFactory;
    [SerializeField] private EnemyContorller _enemyContorller;
    [SerializeField] private EnemyFactory _enemyFactory;
    [SerializeField] private BuildingController _buildingController;
    [SerializeField] private ContentSelector _contentSelector;
    [SerializeField] private ProjectileFactory _warFactory;
    [SerializeField] private GameScenario _sceraio;
    [SerializeField] private Coins _coins;

    private void Start()
    {
        _buildingController.Initialize(_contentFactory);
        _contentSelector.Initialize(_contentFactory);
    }
}
