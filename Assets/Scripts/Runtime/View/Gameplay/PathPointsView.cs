using UnityEngine;

public class PathPointsView : MonoBehaviour, IPathView
{
    [SerializeField] private MovementType _movementType;
    [SerializeField] private float _leastDistance = 0.1f;
    [SerializeField] private Transform[] _pathPoints;
    [SerializeField] private bool _isDraw;

    public Transform[] Points => _pathPoints;
    public Vector3 InitialPoint => _pathPoints[0].position;
    public MovementType MovementType => _movementType;
    public float LeastDistance => _leastDistance;

    private void OnDrawGizmos()
    {
        if (_isDraw)
        {
            if (_pathPoints == null || _pathPoints.Length < 2)
            {
                Debug.Log("No points");
                return;
            }

            for (int i = 1; i < _pathPoints.Length; i++)
            {
                Gizmos.DrawLine(_pathPoints[i - 1].position, _pathPoints[i].position);
            }
        }
    }
}
