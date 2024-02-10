using DG.Tweening;
using System;
using UnityEngine;

[SelectionBase]
public abstract class TowerBase : TileContent
{
    [SerializeField] private TowerType _towerType;
    [SerializeField, Range(1f, 10f)] protected float _targetRange;
    [SerializeField] protected float _speedProjectile = 4f;

    private TargetRadiusView _targetRadiusView;

    public event Action<TileContent> SelectedEvent;

    public readonly uint MaxLevel = 2;
    public TowerType TowerType => _towerType;
    public float TargetRange => _targetRange;
    public uint Level { get; protected set; }
    public uint UpgradeCost {  get; protected set; }
    public uint SellCost { get; protected set; }

    protected ProjectileController _projectileController;

    private BoxCollider _collider;

    public bool IsInit
    {
        get => _collider.enabled;
        set => _collider.enabled = value;
    }

    public void Initialize(TowerConfig config, uint level)
    {
        Level = level;
        UpgradeCost = config.UpgradeCost;
        SellCost = config.SellCost;
        SetStats(config);
        _collider = GetComponent<BoxCollider>();
        _targetRadiusView = GetComponentInChildren<TargetRadiusView>();
        _collider.size = new Vector3(0.9f, 1f, 0.9f);
        _collider.enabled = false;
    }

    public void SetProjectile(ProjectileController projectile)
    {
        _projectileController = projectile;
    }

    public override void Interact()
    {
        SelectedEvent?.Invoke(this);
        ShowTargetRadius(true);
    }

    public override void Undo()
    {
        ShowTargetRadius(false);
    }

    public void ShowTargetRadius(bool isEnable)
    {
        if (isEnable)
        {
            _targetRadiusView.SetRadius(_targetRange);
            _targetRadiusView.Show();
        }
        else
        {
            _targetRadiusView.Hide();
        }
    }

    protected abstract void SetStats(TowerConfig config);

    protected bool IsAcquireTarget(out TargetPoint target)
    {
        if (TargetPoint.FillBuffer(transform.localPosition, _targetRange))
        {
            target = TargetPoint.GetBuffered(0);
            return true;
        }

        target = null;
        return false;
    }

    protected bool IsTargetTracked(ref TargetPoint target)
    {
        if (target == null)
        {
            return false;
        }

        Vector3 myPosition = transform.localPosition;
        Vector3 targetPosition = target.Position;

        float distance = Vector3.Distance(myPosition, targetPosition);
        float range = _targetRange + target.ColliderSize * target.Enemy.Scale;

        if (distance > range || target.IsEnabled == false)
        {
            target = null;
            return false;
        }

        return true;
    }

    protected Vector3 PredictPosition(Vector3 startPosition, Vector3 targetPosition, Vector3 targetVelocity)
    {
        Vector3 targetDistance = targetPosition - startPosition;

        float a = Vector3.Dot(targetPosition, targetPosition) - (_speedProjectile * _speedProjectile);
        float b = 2 * Vector3.Dot(targetVelocity, targetDistance);
        float c = Vector3.Dot(targetDistance, targetDistance);

        float discriminant = Mathf.Sqrt((b * b) - 4 * a * c);

        float t1 = (-b + discriminant) / (2 * a);
        float t2 = (-b - discriminant) / (2 * a);

        float time = Mathf.Max(t1, t2);

        return targetPosition + targetVelocity * time;
    }

    protected ProjectileConfig GetProjectileConfig(Vector3 start, Vector3 target, float damage, float blastRadius = 0f)
    {
        return new ProjectileConfig(start, target, damage, blastRadius);
    }
}