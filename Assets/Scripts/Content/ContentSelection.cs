using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContentSelection : MonoBehaviour
{
    [SerializeField] private InputController _mouseController;
    [SerializeField] private GameBoard _gameBoard;

    public event Action OnBuildingMenu;
    public event Action OnTowerMenu;
    public event Action OnHideMenu;

    private GameTile _tempTile;
    public GameTile TempTile => _tempTile;

    private void Start()
    {
        _mouseController.OnMouseButtonDown += OnSelectContent;
    }

    private GameTile TryGetTile()
    {
        var plane = new Plane(Vector3.up, Vector3.zero);
        if (plane.Raycast(_mouseController.TouchRay, out var distance))
        {
            var position = _mouseController.TouchRay.GetPoint(distance);
            var tile = _gameBoard.GetTile(position);
            return tile;
        }

        return null;
    }

    private void DefineContent(GameTile tile)
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            if (tile != null)
            {
                _tempTile = tile;

                if (tile.Content.Type == GameTileContentType.Place)
                {
                    OnBuildingMenu?.Invoke();
                }
                else if (tile.Content.Type == GameTileContentType.Tower)
                {
                    OnTowerMenu?.Invoke();
                }
                else
                {
                    OnHideMenu?.Invoke();
                }
            }
            else
            {
                _tempTile = null;
                OnHideMenu?.Invoke();
            }
        }
    }

    private void OnSelectContent()
    {
        var tile = TryGetTile();
        DefineContent(tile);
    }
}
