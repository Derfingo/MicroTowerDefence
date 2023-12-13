using UnityEngine;

public class RaycastController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private InputController _input;
    [SerializeField] private LayerMask _touchMask;

    public bool IsHit {  get; private set; }

    private const float MAX_DISTANCE = 100f;
    private Vector3 _hitPosition;

    public Vector3 RaycastPosition()
    {
        var mousePosition = _input.GetMousePosition();
        var ray = _camera.ScreenPointToRay(mousePosition);

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
}
