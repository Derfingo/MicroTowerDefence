using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static InputActionMaps;

namespace MicroTowerDefence
{
    public class ActionMapReader : IPlayerActions, IUIActions, IInputActions
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
        }

        public void Dispose()
        {
            _inputActionMaps.Dispose();
            //Debug.Log($"disposed : {GetType().Name}");
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
            throw new NotImplementedException();
        }

        public void OnSubmit(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnCancel(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnPoint(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnClick(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnScrollWheel(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnMiddleClick(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnTrackedDevicePosition(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnTrackedDeviceOrientation(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnRightClick(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        ~ActionMapReader()
        {
            _inputActionMaps.Dispose();
        }
    }
}