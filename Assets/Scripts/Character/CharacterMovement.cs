using Data;
using UnityEngine;
using UnityEngine.InputSystem;
using Utility.Lerp;

namespace Character
{
    public class CharacterMovement : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Transform _object;
        [SerializeField] private Transform _orientation;
        
        [Space, Header("Edit Data")]
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _moveDecay;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _rotationDecay;
        
        [Space, Header("Runtime Data")]
        [SerializeField] private CharacterRuntimeData _characterRuntimeData;

        private Vector2 _moveInput;
        private Vector2 _rotationInput;

        private bool _jumpInCooldown;
        private float _jumpCooldown;
        private float _jumpTimer;
        
        private void Update()
        {
            _orientation.Rotate(Vector3.up, _rotationInput.x);
            
            var moveDirection = (_orientation.forward * _moveInput.y + _orientation.right * _moveInput.x).normalized;
            var velocityLerpValue = ExponentialDecay.Evaluate(_rigidbody.linearVelocity, moveDirection * _moveSpeed, 
                _moveDecay, Time.deltaTime);
            velocityLerpValue.y = _rigidbody.linearVelocity.y;
            _rigidbody.linearVelocity = velocityLerpValue;
            
            if (!_moveInput.Equals(Vector2.zero))
            {
                var rotationLerpValue = ExponentialDecay.Evaluate(_object.forward, _orientation.forward, 
                    _rotationDecay, Time.deltaTime * _rotationSpeed);

                _object.forward = rotationLerpValue;
            }

            if (_jumpInCooldown)
            {
                _jumpTimer += Time.deltaTime;
            }

            if (_jumpTimer >= _jumpCooldown)
            {
                _jumpInCooldown = false;
            }
            
            // Debug purposes
            _characterRuntimeData.LinearVelocity = _rigidbody.linearVelocity;
        }
        
        #region Actions
        
        public void Move(InputAction.CallbackContext value)
        {
            _moveInput = value.ReadValue<Vector2>();
            
            if(_characterRuntimeData.LogMoveInput)
                Debug.LogError(_moveInput);
        }

        public void Rotate(InputAction.CallbackContext value)
        {            
            _rotationInput = value.ReadValue<Vector2>();
            
            if(_characterRuntimeData.LogLookInput)
                Debug.LogError(_rotationInput);
        }

        public void Jump()
        {
            if (!_jumpInCooldown && IsOnGround())
            {
                _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
                _jumpInCooldown = true;
            }
        }
        
        #endregion

        #region Utilities

        private bool IsOnGround()
        {
            return Physics.Raycast(_rigidbody.position, Vector3.down, out var hit, 1.1f);
        }
        
        private void OnDrawGizmos()
        {
            Debug.DrawRay(_orientation.position, _orientation.forward * 5f, Color.red);
            Debug.DrawRay(_object.position - Vector3.down / 2f, _object.forward * 5f, Color.blue);
        }

        #endregion
    }
}
