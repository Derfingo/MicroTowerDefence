using UnityEngine;

public class RaycastController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _touchMask;
    [SerializeField] private LayerMask _contentMask;

    public bool IsHit {  get; private set; }

    private const float MAX_DISTANCE = 100f;
    private Vector3 _hitPosition;
    private IInputActions _input;

    public void Initialize(IInputActions input)
    {
        _input = input;
    }

    public Vector3 GetPosition()
    {
        var ray = _camera.ScreenPointToRay(_input.MousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, MAX_DISTANCE, _touchMask))
        {
            _hitPosition = hit.point;

            switch (hit.normal.y)
            {
                case 0: IsHit = false; break;
                case 1: IsHit = true; break;
            };
        }
        else
        {
            IsHit = false;
        }

        return _hitPosition;
    }

    public TileContent GetContent()
    {
        var ray = _camera.ScreenPointToRay(_input.MousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, MAX_DISTANCE, _contentMask))
        {
            if (hit.collider.gameObject != null)
            {
                return hit.collider.GetComponent<TileContent>();
            }
        }

        return null;
    }
}
