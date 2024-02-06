using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRaycastModel
{
    bool CheckHit();
    TileContent GetContent();
}
