using Character;
using UnityEngine;
using UnityEngine.Events;

namespace Projectile
{
    public class Arrow : MonoBehaviour, IHittable
    {
        //[SerializeField] private
        [Header("Components")]
        [SerializeField] private Rigidbody _rigidbody;
        
        [Space, Header("Data")]
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _damage;
        [SerializeField] private float _maxChargeDuration;

        [Space, Header("Events")]
        [SerializeField] private UnityEvent _onHitAction;
        
        public void OnHit(GameObject hitGo)
        {
            _onHitAction?.Invoke();
        }
        
        
        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.TryGetComponent<HealthComponent>(out var healthComponent)) return;
            
            healthComponent.Affect(-_damage);
            Destroy(gameObject);
        }
    }
}
