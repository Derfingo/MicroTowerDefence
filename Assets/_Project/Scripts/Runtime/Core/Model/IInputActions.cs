using System;
using UnityEngine;

namespace MicroTowerDefence
{
    public interface IInputActions
    {
        public event Action OnSelectEvent;
        public event Action GamePauseEvent;
        public event Action CancelSelectPlaceEvent;
        public event Action<float> RotateCameraEvent;
        public event Action<float> ScrollEvent;
        public event Action<bool> TowerPlacesEvent;
        public event Action TurnCameraLeftEvent;
        public event Action TurnCameraRightEvent;

        void SetAllMaps();
        void SetPlayerMap();
        void SetUIMap();

        Vector3 MousePosition { get; }
    }
}