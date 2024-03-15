using UnityEngine;

namespace MicroTowerDefence
{
    public sealed class ElementFactor : MonoBehaviour
    {
        private static readonly float[,] Factors = new float[,]
        {
            {0,  1,     2,      3,      4,      5,      6,      7,   8},
            {1,  1,     1.5f,   1.5f,   1.5f,   1.5f,   1.5f,   2,   0},
            {2,  0.5f,  2,      2,      2,      2,      2,      2,   0},
            {3,  0.5f,  2,      0,      2,      2,      2,      2,   0},
            {4,  0.5f,  2,      2,      0,      2,      2,      2,   0},
            {5,  0.5f,  2,      2,      2,      0,      2,      2,   0},
            {6,  0.5f,  2,      2,      2,      2,      0,      2,   0},
            {7,  0.5f,  1,      0.5f,   0.5f,   0.5f,   0.5f,   1,   2}
        };

        public static float GetFactor(ElementType projectile, ElementType enemy)
        {
            return Factors[(int)enemy, (int)projectile];
        }
    }
}