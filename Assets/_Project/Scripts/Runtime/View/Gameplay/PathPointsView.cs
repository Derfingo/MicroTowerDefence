using UnityEngine;

namespace MicroTowerDefence
{
    public class PathPointsView : MonoBehaviour, IPathView
    {
        [SerializeField] private PathConfig _pathconfig;
        [SerializeField] private bool _isDraw;

        public PathConfig GetConfig()
        {
            return _pathconfig;
        }

        private void OnDrawGizmos()
        {
            if (_isDraw)
            {
                if (_pathconfig.Points == null || _pathconfig.Points.Length < 2)
                {
                    Debug.Log("No points");
                    return;
                }

                for (int i = 1; i < _pathconfig.Points.Length; i++)
                {
                    Gizmos.DrawLine(_pathconfig.Points[i - 1].position, _pathconfig.Points[i].position);
                }
            }
        }
    }
}