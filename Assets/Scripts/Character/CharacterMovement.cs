using Data;
using UnityEngine;
using UnityEngine.InputSystem;
using Utility.Lerp;

namespace Character
{
    public class CharacterMovement : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private Rigidbody _characterRigidbody;
        [SerializeField] private Transform _cameraTargetTransform;
        
        [Space, Header("Edit Data")]
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _cameraTurnSpeed;
        [SerializeField] private float _characterTurnSpeed;
        
        [Space, Header("Runtime Data")]
        [SerializeField] private CharacterRuntimeData _characterRuntimeData;

        private Vector2 _moveInput;
        private Vector2 _rotationInput;

        private void Update()
        {
            _cameraTargetTransform.Rotate(Vector3.up, _rotationInput.x * _cameraTurnSpeed * Time.deltaTime);
            
            var targetVelocity = _cameraTargetTransform.forward * _moveInput.y + _cameraTargetTransform.right * _moveInput.x;
            var velocityLerpValue = ExponentialDecay.Evaluate(_characterRigidbody.linearVelocity, 
                targetVelocity, _moveSpeed, Time.deltaTime);

            if (!_characterRigidbody.linearVelocity.Equals(velocityLerpValue))
            {
                var rotationLerpValue = ExponentialDecay.Evaluate(_characterRigidbody.transform.rotation.eulerAngles, 
                    _cameraTargetTransform.rotation.eulerAngles, _characterTurnSpeed, Time.deltaTime);
                
                _characterRigidbody.transform.forward = rotationLerpValue;
                _characterRigidbody.linearVelocity = velocityLerpValue;
            }
            
            // Debug purposes
            _characterRuntimeData.LinearVelocity = _characterRigidbody.linearVelocity;
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
                _characterRigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            }
        }
        
        #endregion

        #region Utilities

        private bool IsOnGround()
        {
            return Physics.Raycast(_characterRigidbody.position, Vector3.down, out var hit, 1.1f);
        }

        #endregion
    }
}
