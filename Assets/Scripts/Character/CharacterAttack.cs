using System;
using Projectile;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Character
{
    public class CharacterAttack : MonoBehaviour
    {
        [SerializeField] private Arrow _projectilePrefab;
        
        [Space, Header("Events")]
        [SerializeField] private UnityEvent _onAttack;
        
        private bool _isCharging;
        private float _chargeDuration;
        
        public void Attack(InputAction.CallbackContext context)
        {
            Debug.LogError(context.phase);
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    _chargeDuration = 0f;
                    _isCharging = true;
                    break;
                case InputActionPhase.Canceled:
                    var spawnPosition = transform.position + transform.forward.normalized;
                    var projectile = Instantiate(_projectilePrefab, spawnPosition, Quaternion.Euler(transform.forward));
                    projectile.Initialize(transform.forward, _chargeDuration);
                    _isCharging = false;
                    _onAttack.Invoke();
                    break;
            }
        }

        private void Update()
        {
            if (_isCharging)
            {
                _chargeDuration += Time.deltaTime;
            }
        }
    }
}
