using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSelection : MonoBehaviour
{
    [SerializeField] private InputController _mouseController;
    [SerializeField] private GameBoard _gameBoard;

    private GameTile _SelectedTile;

    private void Start()
    {
        _mouseController.OnMouseButtonDown += OnSelectContent;
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
                Debug.Log(tile.Content.Type);
            }
        }
    }
}
