using System;
using UnityEngine;

namespace MicroTowerDefence
{
    public interface ITowerController
    {
        public event Action<TileContent> TowerSelectedEvent;

        void OnBuild(TileContent content, Vector3 position);
        void OnSell(TileContent content, uint coins);
        void OnUpgrade(TileContent content);
    }
}