using Data;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
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
                var rotationLerpValue = ExponentialDecay.Evaluate(transform.forward, _orientation.forward, 
                    _rotationDecay, Time.deltaTime * _rotationSpeed);

                _object.forward = rotationLerpValue;
            }
            
            // Debug purposes
            _characterRuntimeData.LinearVelocity = _rigidbody.linearVelocity;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(_orientation.position, _orientation.forward * 5f);
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
            if (IsOnGround())
            {
                _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            }
        }
        
        #endregion

        #region Utilities

        private bool IsOnGround()
        {
            return Physics.Raycast(_rigidbody.position, Vector3.down, out var hit, 1.1f);
        }

        #endregion
    }
}
