using UnityEngine;

public class TargetCellView : ViewBase
{
    [SerializeField] private SpriteRenderer _targetSprite;

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public override void Hide()
    {
        _targetSprite.enabled = false;
    }

    public override void Show()
    {
        _targetSprite.enabled = true;
    }
}
