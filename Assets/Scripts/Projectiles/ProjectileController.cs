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

    public MagicSphere GetSphere()
    {
        var shpere = _factory.MagicSphere;
        _projectiles.Add(shpere);
        return shpere;
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