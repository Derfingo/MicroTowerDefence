using UnityEngine;

public interface IGridModel
{
    float GetHeightTilemap();
    Vector3Int GetWorldGridPosition();
    Vector3 GetCellCenterPosition();
}
