using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageButton : ButtonBase
{
    private Image _image;
    private readonly float _duration = 0.4f;
    private readonly float _fadeInValue = 0.5f;
    private readonly float _fadeOutValue = 1f;

    private void Start()
    {
        _image = GetComponent<Image>();
        PointerEnterEvent += FadeIn;
        PointerExitEvent += FadeOut;
    }

    private void FadeIn()
    {
        _image.DOFade(_fadeInValue, _duration);
    }

    private void FadeOut()
    {
        _image.DOFade(_fadeOutValue, _duration);
    }
}
