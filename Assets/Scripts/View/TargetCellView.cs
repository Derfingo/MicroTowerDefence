using UnityEngine;

public class TargetCellView : ViewBase
{
    [SerializeField] private SpriteRenderer _targetSprite;
    [SerializeField] private GameObject _blockBuild;

    public void SetTargetCellPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void ShowTargetCell()
    {
        _targetSprite.enabled = true;
    }

    public void HideTargerCell()
    {
        _targetSprite.enabled = false;
    }
}
