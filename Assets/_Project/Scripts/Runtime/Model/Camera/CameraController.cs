using UnityEngine;
using Zenject;

namespace MicroTowerDefence
{
    public class CameraController : MonoBehaviour, ILateUpdate
    {
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private bool _isOrthographicCamera = true;
        [Space]
        [SerializeField] private float _moveTime = 10f;
        [SerializeField] private float _zoomVelocity = 0.1f;
        [SerializeField] private float _rotationVelocity = 2.5f;

        public Quaternion Rotation;
        public float Zoom;

        private IInputActions _input;
        private Camera _camera;
        private Quaternion _orthographicRotation = Quaternion.Euler(30f, 45f, 0f);
        private Quaternion _perspectiveRotation = Quaternion.Euler(45f, 0f, 0f);

        [Inject]
        public void Initialize(IInputActions input)
        {
            _input = input;
            _camera = _cameraTransform.GetComponent<Camera>();

            _input.ScrollEvent += OnZoom;
            _input.RotateCameraEvent += OnTurnDelta;

            _input.TurnCameraLeftEvent += OnTurnLeft;
            _input.TurnCameraRightEvent += OnTurnRight;

            Rotation = transform.rotation;
            Zoom = _camera.orthographicSize;

            SetCameraType(_isOrthographicCamera);
        }

        public void GameLateUpdate()
        {
            HandleMovement();
        }

        private void SetCameraType(bool isOrthographic)
        {
            _camera.orthographic = isOrthographic;
            _cameraTransform.rotation = isOrthographic ? _orthographicRotation : _perspectiveRotation;
        }

        private void OnZoom(float zoom)
        {
            Zoom = Mathf.Clamp(Zoom - zoom * _zoomVelocity, 3f, 5f);
            _camera.orthographicSize = Zoom;
        }

        private void OnTurnLeft()
        {
            if (_isOrthographicCamera)
            {
                Rotation *= Quaternion.Euler(0f, 90f, 0f);
            }
            else
            {
                Rotation *= Quaternion.Euler(Vector3.up * _rotationVelocity / 3);
            }
        }

        private void OnTurnRight()
        {
            if (_isOrthographicCamera)
            {
                Rotation *= Quaternion.Euler(0f, -90f, 0f);
            }
            else
            {
                Rotation *= Quaternion.Euler(Vector3.up * -_rotationVelocity / 3);
            }
        }

        private void OnTurnDelta(float rotation)
        {
            Vector3 turn = new(0f, rotation, 0f);
            Rotation *= Quaternion.Euler(turn * _rotationVelocity);
        }

        private void HandleMovement()
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Rotation, Time.deltaTime * _moveTime);
        }
    }
}