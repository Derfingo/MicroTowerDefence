using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private Transform _targetPoint;
    [SerializeField, Range(100f, 1000f)] private float _speedRotation = 100f;

    private Vector2 _turn;
    private float _min = 0.1f;

    private void Update()
    {
        _turn.x = Input.GetAxis("Mouse X");
        _turn.y = Input.GetAxis("Mouse Y");

        if (Input.GetButton("Fire1"))
        {
            Rotate();
        }
    }

    private void Rotate()
    {
        transform.Rotate(transform.position, _turn.x * _speedRotation * Time.deltaTime);
    }
}
