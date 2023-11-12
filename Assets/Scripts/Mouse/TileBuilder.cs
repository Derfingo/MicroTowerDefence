using System.Collections.Generic;
using UnityEngine;

public class TileBuilder : MonoBehaviour
{
    [SerializeField] private List<BuildButton> _buttons;
    [SerializeField] private InputController _inputController;
    [SerializeField] private UIController _uiController;

    private GameTileContentFactory _contentFactory;
    private GameTileContent _tempTile;
    private GameBoard _gameBoard;
    private bool _isEnabled;

    private void Start()
    {
        _buttons.ForEach(b => b.AddListener(OnBuildingSelected));
        _inputController.OnMouseButtonUp += OnBuild;
        _uiController.OnBuildClick += OnBuildSelect;
    }

    public void Initialize(GameTileContentFactory contentFactory, GameBoard gameBoard)
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

    private void OnBuildingSelected(GameTileContentType type)
    {
        //TODO check money
        _tempTile = _contentFactory.Get(type);
    }

    private void OnBuildSelect(GameTileContentType type, GameTile tile)
    {
        GameTileContent content = _contentFactory.Get(type);
        if (tile != null)
        {
            _gameBoard.TryBuild(tile, content);
        }
    }
}