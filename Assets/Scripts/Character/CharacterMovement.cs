using Data;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character
{
    public class CharacterMovement : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private Rigidbody _object;
        [SerializeField] private Transform _orientation;
        
        [Header("Edit Data")]
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _lookSpeed;
        [SerializeField] private float _jumpForce;
        
        [Header("Runtime Data")]
        [SerializeField] private CharacterRuntimeData _characterRuntimeData;
        
        private Vector3 _lastMoveInput;
        
        private Vector2 _currentLookInput;
        private Vector2 _previousLookInput;

        public void Move(InputAction.CallbackContext value)
        {
            var vector = value.ReadValue<Vector2>();
            _lastMoveInput = new Vector3(vector.x, 0, vector.y) * _moveSpeed;
            
            if(_characterRuntimeData.LogMoveInput)
                Debug.LogError(vector);
        }

        public void Look(InputAction.CallbackContext value)
        {            
            _currentLookInput = value.ReadValue<Vector2>();
            
            if(_characterRuntimeData.LogLookInput)
                Debug.LogError(_currentLookInput);
        }

        public void Jump()
        {
            if (IsOnGround())
            {
                _object.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            }
        }

        private void Update()
        {
            _lastMoveInput.y = _object.linearVelocity.y;
            var moveDirection = _orientation.forward * _lastMoveInput.z + _orientation.right * _lastMoveInput.x;
            moveDirection.y = _lastMoveInput.y;
            
            _object.linearVelocity = Vector3.Lerp(_object.linearVelocity, moveDirection, Time.deltaTime * 100f);
            _object.transform.Rotate(Vector3.up, _currentLookInput.x * _lookSpeed * Time.deltaTime);
            
            // Debug purposes
            _characterRuntimeData.LinearVelocity = _object.linearVelocity;
        }
        
        private bool IsOnGround()
        {
            return Physics.Raycast(_object.position, Vector3.down, out var hit, 1.1f);
        }
    }
}
