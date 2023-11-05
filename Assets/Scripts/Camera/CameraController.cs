using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _moveSpeed = 0.1f;
    [SerializeField] private float _moveTime = 10f;
    [SerializeField] private float _rotationAmount = 2.5f;
    [SerializeField] private Vector3 _zoomAmount = new(0, -0.3f, 0.3f);

    public Vector3 Target;
    public Vector3 Zoom;
    public Quaternion Rotation;

    private void Start()
    {
        Target = transform.position;
        Rotation = transform.rotation;
        Zoom = _cameraTransform.localPosition;
    }

    private void Update()
    {
        HandleMovement();
        HandleKeyboard();
        HandleMouse();
    }

    private void HandleMouse()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            Zoom += Input.mouseScrollDelta.y * _zoomAmount;
        }

        if (Input.GetMouseButton(1))
        {
            float delta = Input.GetAxis("Mouse X");
            Vector3 turn = new(0, delta, 0);
            Rotation *= Quaternion.Euler(turn * _rotationAmount);
        }
    }

    private void HandleKeyboard()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Target += (transform.forward * _moveSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Target += transform.forward * -_moveSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            Target += transform.right * _moveSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            Target += transform.right * -_moveSpeed;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            Rotation *= Quaternion.Euler(Vector3.up * _rotationAmount / 3);
        }
        if (Input.GetKey(KeyCode.E))
        {
            Rotation *= Quaternion.Euler(Vector3.up * -_rotationAmount / 3);
        }
    }

    private void HandleMovement()
    {
        transform.position = Vector3.Lerp(transform.position, Target, Time.deltaTime * _moveTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, Rotation, Time.deltaTime * _moveTime);
        _cameraTransform.localPosition = Vector3.Lerp(_cameraTransform.localPosition, Zoom, Time.deltaTime * _moveTime);
    }
}
