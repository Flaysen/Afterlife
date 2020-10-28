using UnityEngine;

namespace PlayerControl
{
    [RequireComponent(typeof(InputHandler), typeof(CharacterController))]
    public class PlayerController : MonoBehaviour, IController
    {
        [SerializeField] private float _moveSpeed = 3.0f;

        [SerializeField] private float _gravity = 20.0f;

        [SerializeField] private float _jumpUpForce = 4.0f;

        [SerializeField] private float _jumpForwardForce = 2.0f;

        [SerializeField] private float _dashForce = 6.0f;

        [SerializeField] private float _dashTime = 0.2f;

        [SerializeField] private Animator _animator;

        private Vector3 _moveVelocity;

        private Vector3 _moveDirection;

        private Vector3 _pointToLook;

        private bool _isControlCanceled;

        private float _recaptureControl;

        private float _nextDashTime;

        private Camera _mainCamera;

        private InputHandler _input;

        private CharacterController _characterController;

        public Vector3 PointToLook => _pointToLook;

        public bool IsControlDisabled { get; set; }

        private void Awake()
        {
            _mainCamera = Camera.main;

            _input = GetComponent<InputHandler>();

            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            if (Time.time > _recaptureControl)
                RecaptureControl();

            if(!IsControlDisabled)
            {
                if(CheckIfGrounded() && !_isControlCanceled)
                {
                    CalculateMovement();

                    if (_input.IsJumpButtonPressed)
                    {
                        HandleJump();
                        _animator.SetBool("IsJumping", true);
                    }
                    else _animator.SetBool("IsJumping", false);
                       


                    if (_input.IsDashButtonPressed)
                    {
                        HandleDash();
                        _animator.SetBool("IsDashing", true);
                    }
                    else _animator.SetBool("IsDashing", false);            
                }

                HandleRotation();
            }
        
            AddGravityForce();

            HandleMovement();
        }

        private void RecaptureControl() => _isControlCanceled = false;

        private void HandleJump()
        {
            if (CheckIfGrounded())
            {
                _moveVelocity.y = _jumpUpForce;
                _moveVelocity += _jumpForwardForce * _input.MoveInput;                
            }
        }

        private void HandleDash()
        {
            if (CheckIfGrounded() && _input.MoveInput != Vector3.zero)
            {
                if (Time.time > _nextDashTime)
                {
                    _nextDashTime = Time.time + _dashTime;
                    _isControlCanceled = true;
                    _recaptureControl = _nextDashTime;
                    _moveVelocity = _moveDirection * _dashForce;
                }
            }
        }

        private void AddGravityForce() => _moveVelocity.y -= _gravity * Time.deltaTime;

        private void HandleRotation()
        {
            Ray cameraRay = _mainCamera.ScreenPointToRay(_input.MousePoisition);
            Plane groundPlane = new Plane(Vector3.up, transform.position);

            if (groundPlane.Raycast(cameraRay, out float rayLenght))
            {
                _pointToLook = cameraRay.GetPoint(rayLenght);
                Debug.DrawLine(cameraRay.origin, _pointToLook, Color.blue);
                transform.LookAt(new Vector3(_pointToLook.x, transform.position.y, _pointToLook.z));
            }
        }

        private void CalculateMovement()
        {       
            _moveDirection = _input.MoveInput.normalized;
            _moveVelocity = _moveDirection * _moveSpeed;
            if (_moveVelocity != Vector3.zero) _animator.SetFloat("Speed", 1);
            else _animator.SetFloat("Speed", 0);         
        }

        private void HandleMovement() 
        {
            _characterController.Move(_moveVelocity * Time.deltaTime);
            _animator.SetFloat("Speed", 1);
   
        }

        private bool CheckIfGrounded()
        {
            Bounds bounds = _characterController.bounds;
            return Physics.Raycast(bounds.center, Vector3.down, bounds.extents.y + .1f);
        }
    }
}
