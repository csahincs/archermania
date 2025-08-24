using Projectile;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Character
{
    public class CharacterAttack : MonoBehaviour
    {
        [Header("Component References")]
        [SerializeField] private Transform _characterObject;
        [SerializeField] private ArrowPoolSystem _arrowPool;
        
        [Space, Header("Data")]
        [SerializeField] private float _projectileSpeed;
        
        [Space, Header("Events")]
        [SerializeField] private UnityEvent _onAttack;
        
        private bool _isCharging;
        private float _chargeDuration;
        
        public void Attack(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Canceled:
                    var spawnPosition = _characterObject.position + _characterObject.forward.normalized + Vector3.up / 2f;
                    var arrow = _arrowPool.Pool.Get().GetComponent<Rigidbody>();
                    arrow.transform.position = spawnPosition;
                    arrow.transform.forward = _characterObject.forward;
                    arrow.AddForce(_projectileSpeed * arrow.transform.forward, ForceMode.Impulse);
                    _onAttack.Invoke();
                    break;
            }
        }
    }
}
