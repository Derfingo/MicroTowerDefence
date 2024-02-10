using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MicroTowerDefence
{
    public interface IRaycast
    {
        bool CheckHit();
        TileContent GetContent();
    }
}