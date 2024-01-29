using System;
using UnityEngine;

public class InputReader : MonoBehaviour, IInputActions
{
    public event Action<float> OnRotateCameraEvent;
    public event Action<float> OnMouseZoomEvent;
    public event Action OnTurnCameraLeftEvent;
    public event Action OnTurnCameraRightEvent;
    public event Action OnSelectContentEvent;
    public event Action OnBuildContentEvent;
    public event Action OnPauseGameEvent;

    public Vector3 MousePosition { get; private set; }

    public void GameUpdate()
    {
        GetMousePosition();
        OnMouseButtonsHandler();
        OnScroll();
        OnRotate();
    }

    private void GetMousePosition()
    {
        MousePosition = Input.mousePosition;
    }

    private void OnMouseButtonsHandler()
    {
        if (Input.GetMouseButtonUp(0))
        {
            OnBuildContentEvent?.Invoke();
        }

        if (Input.GetMouseButtonDown(0))
        {
            OnSelectContentEvent?.Invoke();
        }

        if (Input.GetKey(KeyCode.Q))
        {
            OnTurnCameraLeftEvent?.Invoke();
        }

        if (Input.GetKey(KeyCode.E))
        {
            OnTurnCameraRightEvent?.Invoke();
        }
    }

    private void OnScroll()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            OnMouseZoomEvent?.Invoke(Input.mouseScrollDelta.y);
        }
        else
        {
            OnMouseZoomEvent?.Invoke(0f);
        }
    }

    private void OnRotate()
    {
        if (Input.GetMouseButton(1))
        {
            OnRotateCameraEvent?.Invoke(Input.GetAxis("Mouse X"));
        }
    }
}
