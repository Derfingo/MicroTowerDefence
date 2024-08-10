using DG.Tweening;
using UnityEngine;
using Zenject;

namespace MicroTowerDefence
{
    public class CursorView : ViewBase, ICursorView
    {
        private MeshRenderer _cursorMesh;

        private Tween _tween;

        [Inject]
        public void Initialize()
        {
            _cursorMesh = GetComponentInChildren<MeshRenderer>();
            _cursorMesh.enabled = false;
            Pulse();
        }

        public void UpdateCursor(Vector3 position, bool isShow)
        {
            transform.position = position;
            _cursorMesh.enabled = isShow;
        }

        public void Pulse()
        {
            _tween = _cursorMesh.transform.DOScale(Vector3.one * 1.1f, 0.3f).SetLoops(-1, LoopType.Yoyo)
                .SetLink(gameObject);
        }
    }
}