using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public event Action OnMouseButtonDown;
    public event Action OnMouseButtonUp;

    private Vector3 _scrollDelta;
    private Camera _camera;
    private float _deltaX;

    public float DeltaX => _deltaX;
    public Vector3 ScrollDelta => _scrollDelta;
    public Ray TouchRay => _camera.ScreenPointToRay(Input.mousePosition);

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        OnClick();
        ReadDelta();
    }

    private void OnClick()
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

    private void ReadDelta()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            _scrollDelta = Input.mouseScrollDelta;
        }
        else
        {
            _scrollDelta.y = 0;
        }

        if (Input.GetMouseButton(1))
        {
            _deltaX = Input.GetAxis("Mouse X");
        }
    }
}
