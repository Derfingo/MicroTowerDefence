using System;
using UnityEngine;

namespace MicroTowerDefence
{
    public interface IInputActions
    {
        public event Action OnSelectEvent;
        public event Action OnPauseEvent;
        public event Action<float> OnRotateCameraEvent;
        public event Action<float> OnScrollEvent;
        public event Action OnTurnCameraLeftEvent;
        public event Action OnTurnCameraRightEvent;

        void Dispose();
        void Disable();
        void Enable();

        void SetUIInput();
        void SetPlayerInput();

        Vector3 MousePosition { get; }
    }
}