using DG.Tweening;
using UnityEngine;

public class TowerPlaceView : ViewBase
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private readonly float _duration = 0.1f;

    public override void Show()
    {
        _spriteRenderer.DOFade(1f, _duration);
    }

    public override void Hide()
    {
        _spriteRenderer.DOFade(0f, _duration);
    }
}
