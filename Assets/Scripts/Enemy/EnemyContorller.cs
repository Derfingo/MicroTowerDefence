using UnityEngine;

public class EnemyContorller : MonoBehaviour
{
    [SerializeField] private PathPointsView _enemyPathView;

    private readonly GameBehaviourCollection _enemies = new();

    public bool IsEmpty => _enemies.IsEmpty;

    public void Spawn(EnemyFactory factory, EnemyType type)
    {
        Enemy enemy = factory.Get(type);
        enemy.SetPath(_enemyPathView);
        enemy.SetPosition(_enemyPathView.InitialPoint);
        _enemies.Add(enemy);
    }

    public void Clear()
    {
        _enemies.Clear();
    }

    public void GameUpdate()
    {
        _enemies.GameUpdate();
    }
}