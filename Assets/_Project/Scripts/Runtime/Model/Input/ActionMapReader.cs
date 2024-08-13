using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static InputActionMaps;

namespace MicroTowerDefence
{
    public class ActionMapReader : IPlayerActions, IUIActions, IInputActions, IDisposable
    {
        public event Action<float> RotateCameraEvent;
        public event Action<float> ScrollEvent;
        public event Action TurnCameraLeftEvent;
        public event Action TurnCameraRightEvent;
        public event Action GamePauseEvent;
        public event Action OnSelectEvent;

        public Vector3 MousePosition { get; private set; }

        private readonly InputActionMaps _inputActionMaps;
        private bool _isRotateCamera;

        public ActionMapReader()
        {
            _inputActionMaps = new InputActionMaps();
            _inputActionMaps.Player.SetCallbacks(this);
            _inputActionMaps.UI.SetCallbacks(this);
        }

        public void SetUIInput()
        {
            _inputActionMaps.Player.Disable();
            _inputActionMaps.UI.Enable();
        }

        public void SetPlayerInput()
        {
            _inputActionMaps.UI.Disable();
            _inputActionMaps.Player.Enable();
        }

        public void Enable()
        {
            _inputActionMaps.Enable();
        }

        public void Disable()
        {
            _inputActionMaps.Disable();
        }

        // Gamaplay Actions

        public void OnMousePosition(InputAction.CallbackContext context)
        {
            MousePosition = context.ReadValue<Vector2>();
        }

        public void OnMouseScroll(InputAction.CallbackContext context)
        {
            ScrollEvent?.Invoke(context.ReadValue<Vector2>().y);
        }

        public void OnTurnCameraLeft(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                TurnCameraLeftEvent?.Invoke();
            }
        }

        public void OnTurnCameraRight(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                TurnCameraRightEvent?.Invoke();
            }
        }

        public void OnRotateCamera(InputAction.CallbackContext context)
        {
            if (_isRotateCamera)
            {
                RotateCameraEvent?.Invoke(context.ReadValue<Vector2>().x);
            }
        }

        public void OnRightMouseButtonHold(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                _isRotateCamera = true;
            }

            if (context.canceled)
            {
                _isRotateCamera = false;
            }
        }

        public void OnSelectPlace(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                OnSelectEvent?.Invoke();
            }
        }

        public void OnGamePause(InputAction.CallbackContext context)
        {
            GamePauseEvent?.Invoke();
        }

        // Ui Actions

        public void OnNavigate(InputAction.CallbackContext context)
        {
        }

        public void OnSubmit(InputAction.CallbackContext context)
        {
        }

        public void OnCancel(InputAction.CallbackContext context)
        {
        }

        public void OnPoint(InputAction.CallbackContext context)
        {
        }

        public void OnClick(InputAction.CallbackContext context)
        {
        }

        public void OnScrollWheel(InputAction.CallbackContext context)
        {
        }

        public void OnMiddleClick(InputAction.CallbackContext context)
        {
        }

        public void OnTrackedDevicePosition(InputAction.CallbackContext context)
        {
        }

        public void OnTrackedDeviceOrientation(InputAction.CallbackContext context)
        {
        }

        public void OnRightClick(InputAction.CallbackContext context)
        {
        }

        public void Dispose()
        {
            _inputActionMaps.Dispose();
            Debug.Log("dispose");
        }

        ~ActionMapReader()
        {
            _inputActionMaps.Dispose();
        }
    }
}