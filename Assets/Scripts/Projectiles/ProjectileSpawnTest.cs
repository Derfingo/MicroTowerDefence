using UnityEngine;

public class ProjectileSpawnTest : MonoBehaviour
{
    [SerializeField] private Arrow _projectilePrefab;
    [SerializeField] private Transform _target;

    [SerializeField] private float _spawnTime = 1f;
    private float _time;

    private void Update()
    {
        _time += Time.deltaTime;

        if(_time > _spawnTime)
        {
            var p = Instantiate(_projectilePrefab);
            //p.Initialize(transform.position, _target.position,  ,0f);
            _time = 0f;
        }
    }
}
