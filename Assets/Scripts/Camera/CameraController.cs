using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float moveSpeed = 0.1f;
    [SerializeField] private float moveTime = 10f;
    [SerializeField] private float rotationAmount = 2.5f;
    [SerializeField] private Vector3 zoomAmount = new(0, -0.3f, 0.3f);

    public Vector3 target;
    public Vector3 zoom;
    public Quaternion rotation;

    private void Start()
    {
        target = transform.position;
        rotation = transform.rotation;
        zoom = cameraTransform.localPosition;
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
            zoom += Input.mouseScrollDelta.y * zoomAmount;
        }
        if (Input.GetMouseButton(1))
        {
            float delta = Input.GetAxis("Mouse X");
            Vector3 turn = new(0, delta, 0);
            rotation *= Quaternion.Euler(turn * rotationAmount);
        }
    }

    private void HandleKeyboard()
    {
        if (Input.GetKey(KeyCode.W))
        {
            target += (transform.forward * moveSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            target += transform.forward * -moveSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            target += transform.right * moveSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            target += transform.right * -moveSpeed;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            rotation *= Quaternion.Euler(Vector3.up * rotationAmount / 3);
        }
        if (Input.GetKey(KeyCode.E))
        {
            rotation *= Quaternion.Euler(Vector3.up * -rotationAmount / 3);
        }
    }

    private void HandleMovement()
    {
        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * moveTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * moveTime);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, zoom, Time.deltaTime * moveTime);
    }
}
