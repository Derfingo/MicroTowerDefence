using System.Collections.Generic;
using UnityEngine;

public class TileBuilder : MonoBehaviour
{
    [SerializeField] private List<BuildContentButton> _contentButtons;
    [SerializeField] private List<BuildTowerButton> _towerButtons;
    [SerializeField] private InputController _inputController;
    [SerializeField] private UIController _uiController;
    [SerializeField] private Coins _coins;

    private TileContentFactory _contentFactory;
    private TileContent _tempTile;
    private GameBoard _gameBoard;
    private bool _isEnabled;

    private void Start()
    {
        _contentButtons.ForEach(b => b.AddListener(OnBuildingSelected));
        _towerButtons.ForEach(b => b.AddListener(OnBuildingSelected));
        _inputController.OnMouseButtonUp += OnBuild;
        _uiController.OnBuildClick += OnBuildSelect;
        _uiController.OnSellClick += OnSellTower;

    }

    public void Initialize(TileContentFactory contentFactory, GameBoard gameBoard)
    {
        _contentFactory = contentFactory;
        _gameBoard = gameBoard;
    }

    public void Enable()
    {
        _isEnabled = true;
    }

    public void Disable()
    {
        _isEnabled = false;
    }

    private void Update()
    {
        if (_isEnabled == false || _tempTile == null)
        {
            return;
        }

        var plane = new Plane(Vector3.up, Vector3.zero);

        if (plane.Raycast(_inputController.TouchRay, out var position))
        {
            _tempTile.transform.position = _inputController.TouchRay.GetPoint(position);
        }
    }

    private void OnBuild()
    {
        if (_tempTile == null)
        {
            return;
        }

        var tile = _gameBoard.GetTile(_tempTile.transform.localPosition);
        

        if (tile != null && _gameBoard.TryBuild(tile, _tempTile))
        {
            // spend money
            // record tile
        }
        else
        {
            Destroy(_tempTile.gameObject);
            //Debug.Log("null");
        }

        _tempTile = null;
    }

    private void OnBuildingSelected(TileContentType type)
    {
        _tempTile = _contentFactory.Get(type);
    }

    private void OnBuildingSelected(TowerType type)
    {
        // fix
        if (_coins.Check(10000))
        {
            _tempTile = _contentFactory.Get(type);
        }
    }

    private void OnBuildSelect(TowerType type, GameTile tile)
    {
        Tower content = _contentFactory.Get(type).GetComponent<Tower>();
        if (tile != null)
        {
            if (_coins.Check(content.Cost))
            {
                _gameBoard.TryBuild(tile, content);
                _coins.Spend(content.Cost);
            }
            else
            {
                content.Recycle();
            }
        }
    }

    private void OnSellTower(GameTile tile)
    {
        _gameBoard.DestroyTile(tile);
    }
}