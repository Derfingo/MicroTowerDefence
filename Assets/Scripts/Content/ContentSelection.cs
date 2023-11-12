using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSelection : MonoBehaviour
{
    [SerializeField] private BuildingMenuUI _buildingMenu;
    [SerializeField] private TowerMenuUI _towerMenu;
    [SerializeField] private InputController _mouseController;
    [SerializeField] private GameBoard _gameBoard;

    public event Action<GameTileContentType, GameTile> OnBuild;

    private GameTile _tempTile;

    private void Start()
    {
        _mouseController.OnMouseButtonDown += OnSelectContent;
        _buildingMenu.Buttons.ForEach(b => b.AddListener(OnBuildClick));
    }

    private void OnSelectContent()
    {
        var plane = new Plane(Vector3.up, Vector3.zero);

        if (plane.Raycast(_mouseController.TouchRay, out var position))
        {
            var tilePosition = _mouseController.TouchRay.GetPoint(position);
            var tile = _gameBoard.GetTile(tilePosition);

            if (tile != null)
            {
                if (tile.Content.Type == GameTileContentType.Place)
                {
                    if (_tempTile != null)
                    {
                        _towerMenu.Hide();
                        _buildingMenu.Hide();
                    }

                    _buildingMenu.Show();
                    _tempTile = tile;
                }
                else if (tile.Content.Type == GameTileContentType.Tower)
                {
                    if (_tempTile != null)
                    {
                        _towerMenu.Hide();
                        _buildingMenu.Hide();
                    }

                    _towerMenu.Show();
                    _tempTile = tile;
                }
                else
                {
                    _towerMenu.Hide();
                    _buildingMenu.Hide();
                }
            }
            else
            {
                _towerMenu.Hide();
                _buildingMenu.Hide();
                _tempTile = null;
            }
        }
    }

    private void OnBuildClick(GameTileContentType type)
    {
        OnBuild?.Invoke(type, _tempTile);
    }
}
