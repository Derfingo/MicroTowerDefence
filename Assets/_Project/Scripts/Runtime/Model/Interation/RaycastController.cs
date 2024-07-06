using System;
using UnityEngine;
using Zenject;

namespace MicroTowerDefence
{
    public class RaycastController : MonoBehaviour, IRaycast
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private LayerMask _groundMask;
        [SerializeField] private LayerMask _contentMask;

        public event Action<bool> OnGround;

        private const float MAX_DISTANCE = 100f;
        private Vector3 _hitPosition;
        private IInputActions _input;
        private bool _isHit;

        [Inject]
        public void Initialize(IInputActions input)
        {
            _input = input;
        }

        public bool CheckHit() => _isHit;

        public Vector3 GetPosition()
        {
            var ray = _camera.ScreenPointToRay(_input.MousePosition);
            bool isHit = false;

            if (Physics.Raycast(ray, out RaycastHit hit, MAX_DISTANCE, _groundMask))
            {
                _hitPosition = hit.point;

                switch (hit.normal.y)
                {
                    case 0: isHit = false; break;
                    case 1: isHit = true; break;
                };
            }
            else
            {
                isHit = false;
            }

            if (_isHit != isHit)
            {
                _isHit = isHit;
                OnGround?.Invoke(_isHit);
            }

            return _hitPosition;
        }

        public bool GetContent(out TileContent content)
        {
            var ray = _camera.ScreenPointToRay(_input.MousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, MAX_DISTANCE, _contentMask))
            {
                if (hit.collider.TryGetComponent(out TileContent tileContent))
                {
                    content = tileContent;
                    return true;
                }
            }

            content = null;
            return false;
        }
    }
}