using UnityEngine;

public interface ITowerControllerModel
{
    void OnBuild(TileContent content, Vector3 position);
    void OnSell(TileContent content, uint coins);
    void OnUpgrade(TileContent content);
}
