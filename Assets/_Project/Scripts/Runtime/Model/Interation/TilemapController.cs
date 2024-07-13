using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace MicroTowerDefence
{
    public class TilemapController : MonoBehaviour, IGrid, IUpdate
    {
        [SerializeField] private Tilemap[] _tilemapArray;

        private RaycastController _raycast;
        private Vector3Int _worldGridPosition;
        private Tilemap _targetTilemap;

        [Inject]
        public void Initialize(RaycastController raycastController)
        {
            _raycast = raycastController;
            _targetTilemap = _tilemapArray[0];
        }

        public void GameUpdate()
        {
            var position = _raycast.GetPosition();
            DetectPosition(position);
        }

        public Vector3 GetCellCenterPosition()
        {
            return _targetTilemap.GetCellCenterWorld(_worldGridPosition);
        }

        private void DetectPosition(Vector3 position)
        {
            if (GetTilamap(position.y))
            {
                GetGridPosition(position);
            }
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

        private void GetGridPosition(Vector3 position)
        {
            _worldGridPosition = _targetTilemap.WorldToCell(position);
        }
    }
}