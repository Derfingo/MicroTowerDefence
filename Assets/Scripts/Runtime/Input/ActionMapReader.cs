using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static InputActionMaps;

public class ActionMapReader : MonoBehaviour, IPlayerActions, IUIActions, IInputActions
{
    public event Action<float> OnRotateCameraEvent;
    public event Action<float> OnMouseZoomEvent;
    public event Action OnTurnCameraLeftEvent;
    public event Action OnTurnCameraRightEvent;
    public event Action OnSelectContentEvent;
    public event Action OnBuildContentEvent;
    public event Action OnPauseGameEvent;
    public Vector3 MousePosition { get; private set; }

    private InputActionMaps _inputActionMaps;

    private void Start()
    {
        _inputActionMaps = new InputActionMaps();
        //_inputActionMaps.Player.SetCallbacks(this);
        //_inputActionMaps.UI.SetCallbacks(this);
        AddListeners();
        _inputActionMaps.Player.Enable();
        //_inputActionMaps.UI.Enable();
    }

    private void AddListeners()
    {
        _inputActionMaps.Player.ZoomCamera.performed += OnZoomCamera;
        _inputActionMaps.Player.ZoomCamera.started += OnZoomCamera;
    }

    public void SetPlayerMap()
    {
        _inputActionMaps.UI.Disable();
        _inputActionMaps.Player.Enable();
    }

    public void SetUIMap()
    {
        _inputActionMaps.Player.Disable();
        _inputActionMaps.UI.Enable();
    }

    public void OnMousePosition(InputAction.CallbackContext context)
    {
        MousePosition = context.ReadValue<Vector3>();
    }

    public void OnRotateCamera(InputAction.CallbackContext context)
    {
        OnRotateCameraEvent?.Invoke(context.ReadValue<Vector2>().x);
    }

    public void OnTurnCameraLeft(InputAction.CallbackContext context)
    {
        OnTurnCameraLeftEvent?.Invoke();
    }

    public void OnTurnCameraRight(InputAction.CallbackContext context)
    {
        OnTurnCameraRightEvent?.Invoke();
    }

    public void OnZoomCamera(InputAction.CallbackContext context)
    {
        OnMouseZoomEvent?.Invoke(context.ReadValue<Vector2>().y);
    }

    public void OnSelectContent(InputAction.CallbackContext context)
    {
        if (context.action.WasPressedThisFrame())
        {
            OnSelectContentEvent?.Invoke();
        }
    }

    public void OnBuildContent(InputAction.CallbackContext context)
    {
        if (context.action.WasReleasedThisFrame())
        {
            OnBuildContentEvent?.Invoke();
        }
    }

    public void OnPauseGame(InputAction.CallbackContext context)
    {
    }
}
