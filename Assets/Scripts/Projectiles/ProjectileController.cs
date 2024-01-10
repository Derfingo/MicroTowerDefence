using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private ProjectileFactory _factory;

    private readonly GameBehaviourCollection _projectiles = new();

    public Shell GetShell()
    {
        var shell = _factory.Shell;
        _projectiles.Add(shell);
        return shell;
    }

    public Explosion GetExplosion()
    {
        var explosion = _factory.Explosion;
        _projectiles.Add(explosion);
        return explosion;
    }

    public Arrow GetArrow()
    {
        var arrow = _factory.Arrow;
        _projectiles.Add(arrow);
        return arrow;
    }

    public void GameUpdate()
    {
        _projectiles.GameUpdate();
    }

    public void Clear()
    {
        _projectiles.Clear();
    }
}

public enum ProjectileType
{
    Arrow,
    Shell,
    Explosion,
    Sphere,
    Test
}
