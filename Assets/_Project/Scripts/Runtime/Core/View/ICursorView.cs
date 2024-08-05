using UnityEngine;

namespace MicroTowerDefence
{
    public interface ICursorView
    {
        void UpdateCursor(Vector3 position, bool isShow);
    }
}