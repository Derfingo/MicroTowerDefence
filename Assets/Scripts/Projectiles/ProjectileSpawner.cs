using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] private ProjectileFactory _factory;

    private static ProjectileSpawner _instance;

    private void OnEnable()
    {
        _instance = this;
    }

    public static MagicSphere SpawnMagicSphere()
    {
        var magicSphere = _instance._factory.MagicSphere;
        return magicSphere;
    }
}
