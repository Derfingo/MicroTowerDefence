using DG.Tweening;
using UnityEngine;

public class TowerPlaceView : ViewBase
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private readonly float _duration = 0.1f;

    public void Display(bool isEnable)
    {
        if (isEnable)
        {
            _spriteRenderer.DOFade(1f, _duration);
        }
        else
        {
            _spriteRenderer.DOFade(0f, _duration);
        }
    }
}