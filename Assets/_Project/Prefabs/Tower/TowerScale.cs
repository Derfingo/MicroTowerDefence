using UnityEngine;

namespace MicroTowerDefence
{
    public class TowerScale : MonoBehaviour
    {
        [SerializeField] private float _scale;

        [SerializeField] private TowerLevelConfig[] _configs;

        private void Awake()
        {
            foreach (var config in _configs)
            {
                config.Scale = _scale;
            }
        }
    }
}
