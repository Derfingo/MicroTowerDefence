using UnityEngine;

namespace MicroTowerDefence
{
    public interface IPreview
    {
        void OnShowPreview(TowerType type, Vector3 position);
        void OnBuilPreview();
        void OnHidePreview();
    }
}
