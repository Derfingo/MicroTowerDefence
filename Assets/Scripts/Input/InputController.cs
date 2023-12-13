using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public event Action OnMouseButtonDown;
    public event Action OnMouseButtonUp;

    public float ScrollDeltaY {  get; private set; }
    public float MouseDeltaX { get; private set; }
    private Camera _camera;
    public Ray TouchRay => _camera.ScreenPointToRay(Input.mousePosition);

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        ReadScroll();
        OnMouseClick();
        ReadMouseDelta();
    }

    public Vector3 GetMousePosition()
    {
        return Input.mousePosition;
    }

    private void OnMouseClick()
    {
        if (Input.GetMouseButtonUp(0))
        {
            OnMouseButtonUp?.Invoke();
        }

        if (Input.GetMouseButtonDown(0))
        {
            OnMouseButtonDown?.Invoke();
        }
    }

    private void ReadScroll()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            ScrollDeltaY = Input.mouseScrollDelta.y;
        }
        else
        {
            ScrollDeltaY = 0f;
        }
    }

    private void ReadMouseDelta()
    {
        if (Input.GetMouseButton(1))
        {
            MouseDeltaX = Input.GetAxis("Mouse X");
        }
        else
        {
            MouseDeltaX = 0f;
        }
    }
}
