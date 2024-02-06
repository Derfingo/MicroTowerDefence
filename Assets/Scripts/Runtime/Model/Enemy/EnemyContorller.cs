using System;
using UnityEngine;

public class EnemyContorller : MonoBehaviour
{
    [SerializeField] private PathPointsView _enemyPathView; // view

    public event Action<uint> OnEnemyFinish;
    public event Action<uint> OnEnemyDie;

    private readonly GameBehaviourCollection _enemies = new();

    public bool IsEmpty => _enemies.IsEmpty;

    public void Spawn(EnemyFactory factory, EnemyType type)
    {
        Enemy enemy = factory.Get(type);
        enemy.SetPath(_enemyPathView);
        enemy.SetPosition(_enemyPathView.InitialPoint);
        enemy.OnFinish += OnEnemyFinish;
        enemy.OnDie += OnEnemyDie;
        _enemies.Add(enemy);
    }

    public void SetPause(bool isPause)
    {
        foreach (var enemy in _enemies.Behaviours)
        {
            enemy.GetComponentInChildren<Animator>().enabled = !isPause;
        }
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