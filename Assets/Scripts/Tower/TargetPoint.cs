using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPoint : MonoBehaviour
{
    public Vector3 Position => transform.position;
    public Enemy Enemy {  get; private set; }
    public bool IsEnabled { get; set; } = true;
    public float ColliderSize { get; private set; }
    public static int BufferedCount { get; private set; }

    private const int ENEMY_LAYER_MASK = 1 << 9;
    private static readonly Collider[] _buffer = new Collider[100];

    private void Awake()
    {
        Enemy = transform.root.GetComponent<Enemy>();
        ColliderSize = GetComponent<SphereCollider>().radius * transform.localScale.x;
    }

    public static bool FillBuffer(Vector3 position, float range)
    {
        Vector3 top = position;
        top.y += 3f;
        BufferedCount = Physics.OverlapCapsuleNonAlloc(position, top, range, _buffer, ENEMY_LAYER_MASK);
        return BufferedCount > 0;
    }

    public static TargetPoint GetBuffered(int index)
    {
        var target = _buffer[index].GetComponent<TargetPoint>();
        return target;
    }
}