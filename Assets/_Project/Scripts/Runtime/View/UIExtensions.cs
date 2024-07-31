using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public static class UIExtensions
{
    private static readonly float _duration = 0.3f;
    private static readonly float _fadeOut = 1f;
    private static readonly float _fadeIn = 0f;

    public async static void Show(this Selectable selectable)
    {
        selectable.gameObject.SetActive(true);
        await selectable.GetComponent<RectTransform>()
                .DOScale(_fadeOut, _duration)
                .SetLink(selectable.gameObject)
                .AsyncWaitForKill();
    }

    public async static void Hide(this Selectable selectable)
    {
        await selectable.GetComponent<RectTransform>()
                    .DOScale(_fadeIn, _duration)
                    .SetLink(selectable.gameObject)
                    .AsyncWaitForKill();
        selectable.gameObject.SetActive(false);
    }
}
