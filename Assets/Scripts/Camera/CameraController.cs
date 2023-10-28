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

    public Vector3 _target;
    public Vector3 _zoom;
    public Quaternion _rotation;

    private void Start()
    {
        _target = transform.position;
        _rotation = transform.rotation;
        _zoom = _cameraTransform.localPosition;
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
            _zoom += Input.mouseScrollDelta.y * _zoomAmount;
        }
        if (Input.GetMouseButton(1))
        {
            float delta = Input.GetAxis("Mouse X");
            Vector3 turn = new(0, delta, 0);
            _rotation *= Quaternion.Euler(turn * _rotationAmount);
        }
    }

    private void HandleKeyboard()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _target += (transform.forward * _moveSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            _target += transform.forward * -_moveSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            _target += transform.right * _moveSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            _target += transform.right * -_moveSpeed;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            _rotation *= Quaternion.Euler(Vector3.up * _rotationAmount / 3);
        }
        if (Input.GetKey(KeyCode.E))
        {
            _rotation *= Quaternion.Euler(Vector3.up * -_rotationAmount / 3);
        }
    }

    private void HandleMovement()
    {
        transform.position = Vector3.Lerp(transform.position, _target, Time.deltaTime * _moveTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, _rotation, Time.deltaTime * _moveTime);
        _cameraTransform.localPosition = Vector3.Lerp(_cameraTransform.localPosition, _zoom, Time.deltaTime * _moveTime);
    }
}
