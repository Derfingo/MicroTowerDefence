using System;
using UnityEngine;

namespace MicroTowerDefence
{
    public interface IInputActions
    {
        public event Action OnSelectEvent;
        public event Action GamePauseEvent;
        public event Action<float> RotateCameraEvent;
        public event Action<float> ScrollEvent;
        public event Action TurnCameraLeftEvent;
        public event Action TurnCameraRightEvent;

        void Dispose();
        void Disable();
        void Enable();

        void SetUIInput();
        void SetPlayerInput();

        Vector3 MousePosition { get; }
    }
}