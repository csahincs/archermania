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
        [SerializeField] private Rigidbody _projectilePrefab;
        
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
                    var projectile = Instantiate(_projectilePrefab, spawnPosition, Quaternion.Euler(transform.forward));
                    projectile.AddForce(_projectileSpeed * projectile.transform.forward, ForceMode.Impulse);
                    _onAttack.Invoke();
                    break;
            }
        }
    }
}
