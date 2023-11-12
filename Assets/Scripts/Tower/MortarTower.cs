using UnityEngine;

public class MortarTower : Tower
{
    [SerializeField] private Transform _cannon;
    [SerializeField, Range(1f, 100f)] private float _damage = 50f;
    [SerializeField, Range(0.5f, 3f)] private float _shellBlastRadius = 1f;
    [SerializeField, Range(0.2f, 1f)] private float _shootPerSecond = 1.0f;

    public override GameTileContentType Type => GameTileContentType.Mortar;

    private float _launchSpeed;
    private float _launchProgress;

    private void Awake()
    {
        OnValidate();
    }

    private void OnValidate()
    {
        float x = _targetRange + 0.251f;
        float y = -_cannon.position.y;
        _launchSpeed = Mathf.Sqrt(9.81f * (y + Mathf.Sqrt(x * x + y * y)));
    }

    public override void Initialize(TowerLevel level)
    {
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
        Vector3 launchPoint = _cannon.position;
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

        _cannon.localRotation = Quaternion.LookRotation(new Vector3(direction.x, tanTheta, direction.y));

        InitializationGame.SpawnShell().Initialize(launchPoint, targetPoint,
            new Vector3(s * cosTheta * direction.x, s * sinTheta, s * cosTheta * direction.y), _shellBlastRadius, _damage);

        /* visualization
        Vector3 prev = launchPoint, next;
        for (int i = 1; i < 10; i++)
        {
            float t = i / 10f;
            float dx = s * cosTheta * t;
            float dy = s * sinTheta * t - 0.5f * g * t * t;
            next = launchPoint + new Vector3(direction.x * dx, dy, direction.y * dx);
            Debug.DrawLine(prev, next, Color.blue);
            prev = next;
        }

        Debug.DrawLine(launchPoint, targetPoint, Color.red);
        Debug.DrawLine(new Vector3(launchPoint.x, 0.01f, launchPoint.z),
            new Vector3(launchPoint.x + direction.x * x, 0.01f, launchPoint.z + direction.y * x), Color.white);
        */
    }
}
