using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace MicroTowerDefence
{
    public class TilemapController : MonoBehaviour, IGrid, IUpdate, IPause
    {
        [SerializeField] private Tilemap[] _tilemapArray;

        public event Action<Vector3, bool> OnUpdateCursorEvent;

        private RaycastController _raycast;
        private Vector3Int _worldGridPosition;
        private Tilemap _targetTilemap;

        private bool _isPause;

        [Inject]
        public void Initialize(RaycastController raycastController)
        {
            _raycast = raycastController;
            _targetTilemap = _tilemapArray[0];
        }

        public void GameUpdate()
        {
            if (_isPause)
            {
                return;
            }

            var position = _raycast.GetPosition();
            DetectPosition(position);
        }

        public void Pause(bool isPause)
        {
            _isPause = isPause;
        }

        private void DetectPosition(Vector3 position)
        {
            bool isGround = GetTilamap(position.y);
            Vector3 cursorPosition = Vector3.zero;

            if (isGround)
            {
                cursorPosition = GetGridPosition(position);
            }

            OnUpdateCursorEvent?.Invoke(cursorPosition, isGround);
        }

        private bool GetTilamap(float mouseHeight)
        {
            foreach (var tilemap in _tilemapArray)
            {
                float tilemapHeight = tilemap.transform.position.y;

                if (Mathf.Approximately(mouseHeight, tilemapHeight))
                {
                    _targetTilemap = _tilemapArray[0];
                    return true;
                }
            }

            return false;
        }

        private Vector3 GetGridPosition(Vector3 position)
        {
            _worldGridPosition = _targetTilemap.WorldToCell(position);
            return _targetTilemap.GetCellCenterWorld(_worldGridPosition);
        }
    }
}