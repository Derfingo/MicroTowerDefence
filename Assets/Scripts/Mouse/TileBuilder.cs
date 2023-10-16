using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBuilder : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private GameBoard _board;
    [SerializeField] private GameTileContentFactory _contentFactory;

    public Ray TouchRay => _camera.ScreenPointToRay(Input.mousePosition);

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleTouchWall();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            HandleTouchDestination();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            HandelTouchSpawn();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            HandleTouchNone();
        }
    }

    private void HandleTouchWall()
    {
        GameTile tile = _board.GetTile(TouchRay);
        if (tile != null)
        {
            GameTileContent content = _contentFactory.Get(GameTileContentType.Wall);
            _board.TryBuild(tile, content);
            Debug.Log("Left click");
        }
    }

    private void HandleTouchDestination()
    {
        GameTile tile = _board.GetTile(TouchRay);
        if (tile != null)
        {
            var content = _contentFactory.Get(GameTileContentType.Destination);
            _board.TryBuild(tile, content);
            Debug.Log("Right click");
        }
    }

    private void HandelTouchSpawn()
    {
        GameTile tile = _board.GetTile(TouchRay);
        if (tile != null)
        {
            var content = _contentFactory.Get(GameTileContentType.Spawn);
            _board.TryBuild(tile, content);
            Debug.Log("Space");
        }
    }

    private void HandleTouchNone()
    {
        GameTile tile = _board.GetTile(TouchRay);
        if (tile != null)
        {
            var content = _contentFactory.Get(GameTileContentType.None);
            _board.TryBuild(tile, content);
            Debug.Log("E");
        }
    }
}
