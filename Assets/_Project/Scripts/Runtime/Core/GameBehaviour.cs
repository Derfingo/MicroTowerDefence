using UnityEngine;

namespace MicroTowerDefence
{
    public abstract class GameBehaviour : MonoBehaviour
    {
        public virtual bool GameUpdate()
        {
            return true;
        }

        public abstract void Reclaim(float delay = 0);
    }
}