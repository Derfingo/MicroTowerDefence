using UnityEngine;
using Zenject;

namespace MicroTowerDefence
{
    public class RaycastController : MonoBehaviour, IRaycast
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private LayerMask _groundMask;
        [SerializeField] private LayerMask _contentMask;

        private const float MAX_DISTANCE = 100f;
        private IInputActions _input;
        private Vector3 _position;

        [Inject]
        public void Initialize(IInputActions input)
        {
            _input = input;
        }

        public Vector3 GetPosition()
        {
            var ray = _camera.ScreenPointToRay(_input.MousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, MAX_DISTANCE, _groundMask))
            {
                _position = hit.point;
            }

            return _position;
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