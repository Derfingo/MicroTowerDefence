using UnityEngine;

public class RaycastController : MonoBehaviour, IRaycast
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private LayerMask _contentMask;

    private const float MAX_DISTANCE = 100f;
    private Vector3 _hitPosition;
    private IInputActions _input;
    private bool _isHit;

    public void Initialize(IInputActions input)
    {
        _input = input;
    }

    public bool CheckHit()
    {
        return _isHit;
    }

    public Vector3 GetPosition()
    {
        var ray = _camera.ScreenPointToRay(_input.MousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, MAX_DISTANCE, _groundMask))
        {
            _hitPosition = hit.point;

            switch (hit.normal.y)
            {
                case 0: _isHit = false; break;
                case 1: _isHit = true; break;
            };
        }
        else
        {
            _isHit = false;
        }

        return _hitPosition;
    }

    public TileContent GetContent()
    {
        var ray = _camera.ScreenPointToRay(_input.MousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, MAX_DISTANCE, _contentMask))
        {
            if (hit.collider.TryGetComponent(out TileContent content))
            {
                return content;
            }
        }

        return null;
    }
}