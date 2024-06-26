using UnityEngine;

namespace MicroTowerDefence
{
    public class Explosion : GameBehaviour
    {
        [SerializeField, Range(0f, 1f)] private float _duration = 0.5f;
        [SerializeField] AnimationCurve _scaleCurve;
        [SerializeField] AnimationCurve _colorCurve;

        private static int _colorPropId = Shader.PropertyToID("_Color");
        private static MaterialPropertyBlock _propertyBlock;
        private MeshRenderer _meshRenderer;
        private float _scale;
        private float _age;

        private ElementType _elementType = ElementType.Physical;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        public void Initialize(Vector3 position, float blastRadious, int damage)
        {
            TargetPoint.FillBuffer(position, blastRadious);

            for (int i = 0; i < TargetPoint.BufferedCount; i++)
            {
                TargetPoint.GetBuffered(i).Enemy.TakeDamage(damage, _elementType);
            }

            transform.localPosition = position;
            _scale = 2f * blastRadious;
        }

        public override bool GameUpdate()
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
            _meshRenderer.SetPropertyBlock(_propertyBlock);
            transform.localScale = Vector3.one * (_scale * _scaleCurve.Evaluate(t));

            return true;
        }

        public override void Reclaim(float delay = 0f)
        {
            Destroy(gameObject, delay);
        }
    }
}