using UnityEngine;

public class Arrow : WarEntity
{
    private Vector3 _launchPoint, _targetPoint, _launchVelocity;
    private float _blastRadius, _damage;
    private float _age;

    public void Initialize(Vector3 launchPoint, Vector3 targetPoint, Vector3 launchVelocity, float blastRadius, float damage)
    {
        _launchPoint = launchPoint;
        _targetPoint = targetPoint;
        _launchVelocity = launchVelocity;
        _blastRadius = blastRadius;
        _damage = damage;
    }

    public override bool GameUpdate()
    {
        _age += Time.deltaTime * 2f;
        Vector3 p = _launchPoint + _launchVelocity * _age;
        p.y -= 0.5f * 9.81f * _age * _age;

        if (p.y <= 0.01f)
        {
            InitializationGame.SpawnExplosion().Initialize(_targetPoint, _blastRadius, _damage);
            OriginFactory.Reclaim(this);
            return false;
        }

        transform.localPosition = p;

        Vector3 d = _launchVelocity;
        d.y -= 9.81f * _age;
        transform.localRotation = Quaternion.LookRotation(d);

        InitializationGame.SpawnExplosion().Initialize(p, 0.01f);

        return true;
    }
}
