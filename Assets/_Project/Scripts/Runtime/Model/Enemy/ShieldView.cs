using UnityEngine;

namespace MicroTowerDefence
{
    public class ShieldView : MonoBehaviour, IDamage
    {
        private SphereCollider _collider;
        private MeshRenderer _renderer;
        private float _shieldValue;
        private ElementType _elementType = ElementType.Magic;

        public void Initialize(float shieldValue)
        {
            _collider = GetComponent<SphereCollider>();
            _renderer = GetComponent<MeshRenderer>();

            _shieldValue = shieldValue;
        }

        public void TakeDamage(float value, ElementType type)
        {
            float factor = ElementFactor.GetFactor(type, _elementType);
            float damage = value * factor;
            _shieldValue -= damage;
            //print($"value: {value} factor: {factor} damage: {damage}");
            if (_shieldValue <= 0 )
            {
                gameObject.SetActive(false);
            }
        }
    }
}

