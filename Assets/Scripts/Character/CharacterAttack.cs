using UnityEngine;
using Utility.UniTaskExtension;

namespace Character
{
    public class CharacterAttack : MonoBehaviour
    {
        [SerializeField] private Rigidbody _projectilePrefab;
        [SerializeField] private float _projectileSpeed;
        [SerializeField] private float _cooldown;

        private bool _inCooldown;
        
        public void Attack()
        {
            if (_inCooldown)
                return;
            
            _inCooldown = true;
            
            var spawnPosition = transform.position + transform.forward.normalized;
            var projectile = Instantiate(_projectilePrefab, spawnPosition, 
                Quaternion.Euler(transform.forward));
            projectile.AddForce(_projectileSpeed * transform.forward, ForceMode.Impulse);
            
            UniTaskHelper.WaitAndDo(_cooldown, () => { _inCooldown = false; }, destroyCancellationToken).Forget();
        }
    }
}
