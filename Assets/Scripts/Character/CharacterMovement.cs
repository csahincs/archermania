using System;
using CSToolbox.Runtime.Gizmos;
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
        [SerializeField] private CapsuleCollider _capsuleCollider;
        [SerializeField] private Transform _object;
        [SerializeField] private Transform _orientation;
        
        [Space, Header("Runtime Data")]
        [SerializeField] private CharacterRuntimeData _runtimeData;
        [SerializeField] private CharacterRuntimeDebugData _debugData;

        private Vector2 _moveInput;
        private Vector2 _rotationInput;

        private int _nearWallCount;
        
        private readonly RaycastHit[] _jumpResults = new RaycastHit[5];
        
        private void Update()
        {
            _orientation.Rotate(Vector3.up, _rotationInput.x);

            if (IsOnGround())
            {
                var moveDirection = (_orientation.forward * _moveInput.y + _orientation.right * _moveInput.x).normalized;
                var velocityLerpValue = ExponentialDecay.Evaluate(_rigidbody.linearVelocity, moveDirection * _runtimeData.MoveSpeed, 
                    _runtimeData.MoveDecay, Time.deltaTime);
                velocityLerpValue.y = _rigidbody.linearVelocity.y;
            
                _rigidbody.linearVelocity = velocityLerpValue;
            }
            
            if (!_moveInput.Equals(Vector2.zero))
            {
                var rotationLerpValue = ExponentialDecay.Evaluate(_object.forward, _orientation.forward, 
                    _runtimeData.RotationDecay, Time.deltaTime * _runtimeData.RotationSpeed);

                _object.forward = rotationLerpValue;
            }
            
            // Debug purposes
            _debugData.LinearVelocity = _rigidbody.linearVelocity;
        }
        
        #region Actions
        
        public void Move(InputAction.CallbackContext value)
        {
            _moveInput = value.ReadValue<Vector2>();
            
            if(_debugData.LogMoveInput)
                Debug.LogError(_moveInput);
        }

        public void Rotate(InputAction.CallbackContext value)
        {            
            _rotationInput = value.ReadValue<Vector2>();
            
            if(_debugData.LogLookInput)
                Debug.LogError(_rotationInput);
        }

        public void Jump(InputAction.CallbackContext context)
        {
            if (context.phase != InputActionPhase.Started)
                return;

            var canJump = IsOnGround();
            Vector3 jumpDirection;
            
            if (canJump)
            {
                jumpDirection = Vector3.up;
            }
            else
            {
                (canJump, jumpDirection) = IsTouchingWall();
            }
            
            if(canJump)
                _rigidbody.AddForce(jumpDirection * _runtimeData.JumpForce, ForceMode.Impulse);
        }
        
        #endregion
        
        #region Utilities

        private bool IsOnGround()
        {
            return Physics.Raycast(_rigidbody.position, Vector3.down, out var hit, _runtimeData.GroundCheckDistance);
        }

        private (bool, Vector3) IsTouchingWall()
        {
            var startPoint = _capsuleCollider.transform.position + _capsuleCollider.center - 
                             Vector3.up * _capsuleCollider.height / 4;
            var endPoint = _capsuleCollider.transform.position + _capsuleCollider.center + 
                           Vector3.up * _capsuleCollider.height / 4;
            var doesHit = Physics.CapsuleCast(startPoint, endPoint, _capsuleCollider.radius * 1.5f, 
                Vector3.up, out var hit, 0f);
            
            if(doesHit && !hit.collider.gameObject.CompareTag("Wall"))
                doesHit = false;
            
            return (doesHit, hit.normal);
        }
        
        private void OnDrawGizmos()
        {
            Debug.DrawRay(_orientation.position, _orientation.forward * 5f, Color.red);
            Debug.DrawRay(_object.position - Vector3.down / 2f, _object.forward * 5f, Color.blue);
            
            var startPoint = _capsuleCollider.transform.position + _capsuleCollider.center - 
                             Vector3.up * _capsuleCollider.height / 4;
            var endPoint = _capsuleCollider.transform.position + _capsuleCollider.center + 
                           Vector3.up * _capsuleCollider.height / 4;
            GizmosUtility.DrawCapsuleCast(startPoint, endPoint, _capsuleCollider.radius * 1.5f, Vector3.up, 0f, Color.black);
        }

        #endregion
    }
}
