using System;
using UnityEngine;

public class InputReader : MonoBehaviour, IInputActions
{
    public event Action<float> RotateCameraEvent;
    public event Action<float> ScrollEvent;
    public event Action<bool> TurnCameraLeftEvent;
    public event Action<bool> TurnCameraRightEvent;
    public event Action GamePauseEvent;
    public event Action SelectPlaceEvent;
    public event Action CancelSelectPlaceEvent;

    public Vector3 MousePosition { get; private set; }

    public void GameUpdate()
    {
        GetMousePosition();
        OnKeyboardHandler();
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
        if (Input.GetMouseButtonDown(0))
        {
            SelectPlaceEvent?.Invoke();
        }

        if (Input.GetMouseButtonDown(1))
        {
            CancelSelectPlaceEvent?.Invoke();
        }
    }

    private void OnKeyboardHandler()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TurnCameraLeftEvent?.Invoke(true);
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            TurnCameraLeftEvent?.Invoke(false);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            TurnCameraRightEvent?.Invoke(true);
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            TurnCameraRightEvent?.Invoke(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GamePauseEvent?.Invoke();
        }
    }

    private void OnScroll()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            ScrollEvent?.Invoke(Input.mouseScrollDelta.y);
        }
        else
        {
            ScrollEvent?.Invoke(0f);
        }
    }

    private void OnRotate()
    {
        if (Input.GetMouseButton(1))
        {
            RotateCameraEvent?.Invoke(Input.GetAxis("Mouse X"));
        }
    }

    public void SetPlayerMap()
    {
        Debug.Log("not implemented");
    }

    public void SetUIMap()
    {
        Debug.Log("not implemented");
    }

    public void SetAllMaps()
    {
        Debug.Log("not implemented");
    }
}
