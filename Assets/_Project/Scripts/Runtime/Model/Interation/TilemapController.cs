using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace MicroTowerDefence
{
    public class TilemapController : MonoBehaviour, IGrid, IUpdate
    {
        [SerializeField] private Tilemap[] _tilemapArray;

        private RaycastController _raycast;
        private Dictionary<float, Tilemap> _tilemaps;
        private Vector3Int _worldGridPosition;
        private Tilemap _targetTilemap;
        private float _heightTilemap;

        [Inject]
        public void Initialize(RaycastController raycastController)
        {
            _raycast = raycastController;
            InitializeTilemaps();
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

        private void InitializeTilemaps()
        {
            _tilemaps = new Dictionary<float, Tilemap>();

            for (int i = 0; i < _tilemapArray.Length; i++)
            {
                var height = _tilemapArray[i].transform.position.y;
                _tilemaps.Add(height, _tilemapArray[i]);
            }

            _targetTilemap = _tilemapArray[0];
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
            if (_tilemaps.ContainsKey(mouseHeight))
            {
                _heightTilemap = mouseHeight;
                _targetTilemap = _tilemaps[mouseHeight];
                return true;
            }

            return false;
        }

        private void GetGridPosition(Vector3 position)
        {
            _worldGridPosition = _targetTilemap.WorldToCell(position);
        }
    }
}