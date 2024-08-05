using UnityEngine;
using Zenject;

namespace MicroTowerDefence
{
    public class CursorView : ViewBase, ICursorView
    {
        [SerializeField] private SpriteRenderer _cursorSprite;

        [Inject]
        public void Initialize()
        {
            _cursorSprite.enabled = false;
        }

        public void UpdateCursor(Vector3 position, bool isShow)
        {
            transform.position = position;
            _cursorSprite.enabled = isShow;
        }
    }
}