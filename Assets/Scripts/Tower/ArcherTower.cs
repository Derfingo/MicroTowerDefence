using UnityEngine;

public class ArcherTower : Tower
{
    [SerializeField] private Transform _archer;
    [SerializeField, Range(1f, 100f)] private float _damage = 25f;
    [SerializeField, Range(0.1f, 1f)] private float _shellBlastRadius = 1f;
    [SerializeField, Range(0.2f, 3f)] private float _shootPerSecond = 2.0f;

    private float _launchSpeed;
    private float _launchProgress;

    private void Awake()
    {
        OnValidate();
    }

    private void OnValidate()
    {
        float x = _targetRange + 0.251f;
        float y = -_archer.position.y;
        _launchSpeed = Mathf.Sqrt(9.81f * (y + Mathf.Sqrt(x * x + y * y)));
    }

    protected override void SetStats(TowerConfig config)
    {
        _damage = config.Damage;
        _shootPerSecond = config.ShootPerSecond;
        _shellBlastRadius = config.ShellBlastRadius;
    }

    public override void GameUpdate()
    {
        _launchProgress += Time.deltaTime * _shootPerSecond;

        while (_launchProgress >= 1f)
        {
            if (IsAcquireTarget(out TargetPoint target))
            {
                Launch(target);
                _launchProgress -= 1f;
            }
            else
            {
                _launchProgress = 0.999f;
            }
        }
    }

    private void Launch(TargetPoint target)
    {
        Vector3 launchPoint = _archer.position;
        Vector3 targetPoint = target.Position;
        targetPoint.y = 0f;

        Vector2 direction;
        direction.x = targetPoint.x - launchPoint.x;
        direction.y = targetPoint.z - launchPoint.z;

        float x = direction.magnitude;
        float y = -launchPoint.y;
        direction /= x;

        float g = 9.81f;
        float s = _launchSpeed;
        float s2 = s * s;

        float r = s2 * s2 - g * (g * x * x + 2f * y * s2);
        r = Mathf.Max(0f, r);

        float tanTheta = (s2 + Mathf.Sqrt(r)) / (g * x);
        float cosTheta = Mathf.Cos(Mathf.Atan(tanTheta));
        float sinTheta = cosTheta * tanTheta;

        _archer.localRotation = Quaternion.LookRotation(new Vector3(direction.x, tanTheta, direction.y));

        InitializationGame.SpawnArrow().Initialize(launchPoint, targetPoint,
            new Vector3(s * cosTheta * direction.x, s * sinTheta, s * cosTheta * direction.y), _shellBlastRadius, _damage);
    }
}
