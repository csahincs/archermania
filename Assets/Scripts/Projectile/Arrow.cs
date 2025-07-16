using Character;
using UnityEngine;

namespace Projectile
{
    public class Arrow : MonoBehaviour
    {
        //[SerializeField] private
        [Header("Components")]
        [SerializeField] private Rigidbody _rigidbody;
        
        [Space, Header("Data")]
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _damage;
        [SerializeField] private float _maxChargeDuration;

        public void Initialize(Vector3 direction, float chargeDuration)
        {
            chargeDuration = Mathf.Clamp(chargeDuration, 0, _maxChargeDuration);
            
            var speed = _maxSpeed * (chargeDuration / _maxChargeDuration);
            _rigidbody.AddForce(speed * direction, ForceMode.Impulse);
        }
        
        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.TryGetComponent<HealthComponent>(out var healthComponent)) return;
            
            healthComponent.Affect(-_damage);
            Destroy(gameObject);
        }
    }
}
