using System.Collections.Generic;
using UnityEngine;

namespace MicroTowerDefence
{
    public class DamageConfig : ScriptableObject
    {
        private readonly float _doubleDamage = 2f;

        private Dictionary<DamageType, float> _damageResists = new()
        {
            { DamageType.None, 0f },
            { DamageType.Elemental, 0.8f },
            { DamageType.Physical, 1f },
            { DamageType.Magic, 0.2f },
        };

        public float CalculateResist(float damage, DamageType currentType, DamageType hitType)
        {
            if (currentType == hitType)
            {
                return damage * _doubleDamage;
            }

            return damage * _damageResists[hitType];
        }
    }

    public enum DamageType
    {
        None,
        Elemental, // fire, water, ice, wind, ...
        Physical,
        Magic,
    }
}
