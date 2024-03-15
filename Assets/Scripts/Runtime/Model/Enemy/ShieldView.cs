using UnityEngine;

namespace MicroTowerDefence
{
    public class ShieldView : MonoBehaviour, IDamage
    {
        private SphereCollider _collider;
        private MeshRenderer _renderer;
        private float _shieldValue;

        public void Initialize(float shieldValue)
        {
            _collider = GetComponent<SphereCollider>();
            _renderer = GetComponent<MeshRenderer>();

            _shieldValue = shieldValue;
        }

        public void TakeDamage(float damage)
        {
            _shieldValue -= damage;

            if (_shieldValue <= 0 )
            {
                gameObject.SetActive(false);
            }
        }
    }
}

