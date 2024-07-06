using UnityEngine;

namespace MicroTowerDefence
{
    public class Shell : ProjectileBase
    {
        [SerializeField, Range(0f, 1f)] private float _duration = 0.5f;
        [SerializeField] AnimationCurve _scaleCurve;
        [SerializeField] AnimationCurve _colorCurve;
        [SerializeField] MeshRenderer _shellMeshRenderer;
        [SerializeField] MeshRenderer _explosionMeshRenderer;

        private static int _colorPropId = Shader.PropertyToID("_Color");
        private static MaterialPropertyBlock _propertyBlock;
        private float _scale;
        private float _age;

        public override bool GameUpdate()
        {
            if (_isMoving == false)
            {
                _age += Time.deltaTime;

                if (_age >= _duration)
                {
                    Reclaim();
                    return false;
                }

                _propertyBlock ??= new MaterialPropertyBlock();

                float t = _age / _duration;
                Color c = Color.black;
                c.a = _colorCurve.Evaluate(t);
                _propertyBlock.SetColor(_colorPropId, c);
                _explosionMeshRenderer.SetPropertyBlock(_propertyBlock);
                transform.localScale = Vector3.one * (_scale * _scaleCurve.Evaluate(t));

                return true;
            }

            Move();

            return true;
        }

        protected override void Move()
        {
            if (_isMoving)
            {
                transform.forward = _rigidbBody.velocity;
            }
        }

        protected override void Collide(Collision collision, int layerIndex)
        {
            if (layerIndex == _groundLayer || layerIndex == _enemyLayer)
            {
                _isMoving = false;
                Explode();
            }
        }

        private void Explode()
        {
            _shellMeshRenderer.enabled = false;
            _explosionMeshRenderer.enabled = true;
            _rigidbBody.freezeRotation = true;
            Initialize(transform.position, _blastRadious, _damage);

        }

        private void Initialize(Vector3 position, float blastRadious, int damage)
        {
            TargetPoint.FillBuffer(position, blastRadious);

            for (int i = 0; i < TargetPoint.BufferedCount; i++)
            {
                TargetPoint.GetBuffered(i).Enemy.TakeDamage(damage, _elementType);
            }

            transform.localPosition = position;
            _scale = 2f * blastRadious;
        }
    }
}