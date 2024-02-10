using UnityEngine;

namespace MicroTowerDefence
{
    public class TargetCellView : ViewBase
    {
        [SerializeField] private SpriteRenderer _targetSprite;

        public void UpdatePosition(Vector3 position)
        {
            transform.position = position;
        }

        public void Display(bool isShow)
        {
            _targetSprite.enabled = isShow;
        }
    }
}