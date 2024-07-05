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
        public event Action<bool> TurnCameraLeftEvent;
        public event Action<bool> TurnCameraRightEvent;
        public event Action<bool> TowerPlacesEvent;
        public event Action GamePauseEvent;
        public event Action SelectPlaceEvent;
        public event Action CancelSelectPlaceEvent;

        public Vector3 MousePosition { get; private set; }

        private readonly InputActionMaps _inputActionMaps;
        private bool _isRotateCamera;

        public ActionMapReader()
        {
            _inputActionMaps = new InputActionMaps();
            SetPlayerMap();
        }

        public void SetAllMaps()
        {
            _inputActionMaps.Player.SetCallbacks(this);
            _inputActionMaps.UI.SetCallbacks(this);
            _inputActionMaps.Player.Enable();
            _inputActionMaps.UI.Enable();
        }

        public void SetPlayerMap()
        {
            _inputActionMaps.UI.Disable();
            _inputActionMaps.UI.RemoveCallbacks(this);
            _inputActionMaps.Player.Enable();
            _inputActionMaps.Player.SetCallbacks(this);
            //Debug.Log("player map");
        }

        public void SetUIMap()
        {
            _inputActionMaps.Player.Disable();
            _inputActionMaps.Player.RemoveCallbacks(this);
            _inputActionMaps.UI.Enable();
            _inputActionMaps.UI.SetCallbacks(this);
            //Debug.Log("ui map");
        }

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
                TurnCameraLeftEvent?.Invoke(true);
            }

            if (context.canceled)
            {
                TurnCameraLeftEvent?.Invoke(false);
            }
        }

        public void OnTurnCameraRight(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                TurnCameraRightEvent?.Invoke(true);
            }

            if (context.canceled)
            {
                TurnCameraRightEvent?.Invoke(false);
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
                SelectPlaceEvent?.Invoke();
            }
        }

        public void OnTowerPlaces(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                TowerPlacesEvent?.Invoke(true);
            }

            if (context.canceled)
            {
                TowerPlacesEvent?.Invoke(false);
            }

        }

        public void OnGamePause(InputAction.CallbackContext context)
        {
            GamePauseEvent?.Invoke();
        }

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
            if (context.performed)
            {
            }
        }

        public void OnScrollWheel(InputAction.CallbackContext context)
        {
        }

        public void OnMiddleClick(InputAction.CallbackContext context)
        {
        }

        public void OnRightClick(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                CancelSelectPlaceEvent?.Invoke();
            }
        }

        public void OnTrackedDevicePosition(InputAction.CallbackContext context)
        {
        }

        public void OnTrackedDeviceOrientation(InputAction.CallbackContext context)
        {
        }
    }
}