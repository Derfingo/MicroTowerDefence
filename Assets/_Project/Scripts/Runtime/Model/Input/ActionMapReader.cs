using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static InputActionMaps;

namespace MicroTowerDefence
{
    public class ActionMapReader : IPlayerActions, IUIActions, IInputActions, IDispose
    {
        public event Action<float> OnRotateCameraEvent;
        public event Action<float> OnScrollEvent;
        public event Action OnTurnCameraLeftEvent;
        public event Action OnTurnCameraRightEvent;
        public event Action OnPauseEvent;
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
            OnScrollEvent?.Invoke(context.ReadValue<Vector2>().y);
        }

        public void OnTurnCameraLeft(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                OnTurnCameraLeftEvent?.Invoke();
            }
        }

        public void OnTurnCameraRight(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                OnTurnCameraRightEvent?.Invoke();
            }
        }

        public void OnRotateCamera(InputAction.CallbackContext context)
        {
            if (_isRotateCamera)
            {
                OnRotateCameraEvent?.Invoke(context.ReadValue<Vector2>().x);
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

        // Action for UI and Player

        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                OnPauseEvent?.Invoke();
            }
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
        }

        ~ActionMapReader()
        {
            _inputActionMaps.Dispose();
        }
    }
}