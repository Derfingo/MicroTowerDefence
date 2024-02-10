using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRaycast
{
    bool CheckHit();
    TileContent GetContent();
}
