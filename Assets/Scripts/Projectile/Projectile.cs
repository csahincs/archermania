using Character;
using UnityEngine;

namespace Projectile
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float _damage;

        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.TryGetComponent<HealthComponent>(out var healthComponent)) return;
            
            healthComponent.Affect(-_damage);
            Destroy(gameObject);
        }
    }
}
