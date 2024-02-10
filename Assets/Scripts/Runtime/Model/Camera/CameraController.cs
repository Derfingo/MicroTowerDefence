using UnityEngine;

namespace MicroTowerDefence
{
    public class CameraController : MonoBehaviour, ILateUpdate
    {
        [SerializeField] private Transform _cameraTransform;
        [Space]
        [SerializeField] private bool _isMovement = false;
        [SerializeField] private float _moveSpeed = 0.1f;
        [SerializeField] private float _moveTime = 10f;
        [SerializeField] private float _rotationAmount = 2.5f;
        [SerializeField] private Vector3 _zoomAmount = new(0, -0.3f, 0.3f);

        public Quaternion Rotation;
        public Vector3 Target;
        public Vector3 Zoom;

        private IInputActions _input;
        private bool _isTurnLeft;
        private bool _isTurnRight;

        public void Initialize(IInputActions input)
        {
            _input = input;

            _input.ScrollEvent += OnMouseZoom;
            _input.RotateCameraEvent += OnTurnWithMouse;
            _input.TurnCameraLeftEvent += (isTurnLeft) => _isTurnLeft = isTurnLeft;
            _input.TurnCameraRightEvent += (isTurnRight) => _isTurnRight = isTurnRight;

            Target = transform.position;
            Rotation = transform.rotation;
            Zoom = _cameraTransform.localPosition;
        }

        public void GameLateUpdate()
        {
            HandleKeyboard(_isMovement);
            HandleMovement();
            OnTurnLeft();
            OnTurnRight();
        }

        private void HandleKeyboard(bool isMovement)
        {
            if (isMovement)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    Target += transform.forward * _moveSpeed;
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
            }
        }

        private void OnMouseZoom(float zoom)
        {
            Zoom += zoom * _zoomAmount;
        }

        private void OnTurnLeft()
        {
            if (_isTurnLeft)
            {
                Rotation *= Quaternion.Euler(Vector3.up * _rotationAmount / 3);
            }
        }

        private void OnTurnRight()
        {
            if (_isTurnRight)
            {
                Rotation *= Quaternion.Euler(Vector3.up * -_rotationAmount / 3);
            }
        }

        private void OnTurnWithMouse(float rotation)
        {
            Vector3 turn = new(0f, rotation, 0f);
            Rotation *= Quaternion.Euler(turn * _rotationAmount);
        }

        private void HandleMovement()
        {
            transform.position = Vector3.Lerp(transform.position, Target, Time.deltaTime * _moveTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Rotation, Time.deltaTime * _moveTime);
            _cameraTransform.localPosition = Vector3.Lerp(_cameraTransform.localPosition, Zoom, Time.deltaTime * _moveTime);
        }
    }
}