using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBuilder : MonoBehaviour
{
    [SerializeField] private List<BuildButton> _buttons;

    private GameTileContentFactory _contentFactory;
    private Camera _camera;
    private GameBoard _gameBoard;

    private bool _isEnabled;

    private Ray TouchRay => _camera.ScreenPointToRay(Input.mousePosition);

    private GameTileContent _tempTile;

    private void Awake()
    {
        _buttons.ForEach(b => b.AddListener(OnBuildingSelected));
    }

    public void Initialize(GameTileContentFactory contentFactory, Camera camera, GameBoard gameBoard)
    {
        _contentFactory = contentFactory;
        _camera = camera;
        _gameBoard = gameBoard;
    }

    private void Update()
    {
        if (_isEnabled == false || _tempTile == null)
        {
            return;
        }

        var plane = new Plane(Vector3.up, Vector3.zero);

        if (plane.Raycast(TouchRay, out var position))
        {
            _tempTile.transform.position = TouchRay.GetPoint(position);
        }

        if (Input.GetMouseButtonUp(0))
        {
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
    }

    public void Enable()
    {
        _isEnabled = true;
    }

    public void Disable()
    {
        _isEnabled = false;
    }

    private void OnBuildingSelected(GameTileContentType type)
    {
        //TODO check money
        _tempTile = _contentFactory.Get(type);
    }
}