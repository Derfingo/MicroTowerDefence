using MicroTowerDefence;
using UnityEngine;

[CreateAssetMenu]
public class EnemyWavesConfig : GameObjectFactory
{
    [SerializeField] private Wave[] _waves;

    public Wave[] Waves => _waves;
}
