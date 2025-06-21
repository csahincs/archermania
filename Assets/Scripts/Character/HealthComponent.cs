using Data;
using UnityEngine;

namespace Character
{
    public class HealthComponent : MonoBehaviour
    {
        [Header("Edit Data")]
        [SerializeField] private float _maxHealth;

        [field: SerializeField]
        private float CurrentHealth
        {
            get => _currentHealth;
            set
            {
                _currentHealth = value;
                _characterRuntimeData.Health = _currentHealth;
            }
        }
    
        [Space, Header("Runtime Data")]
        [SerializeField] private CharacterRuntimeData _characterRuntimeData;

        private float _currentHealth;
        
        private void OnEnable()
        {
            Reset();
        }

        public void Affect(float amount)
        {
            CurrentHealth += amount;

            if (CurrentHealth <= 0)
            {
                CurrentHealth = _maxHealth;
            }
        }

        public void Reset()
        {
            CurrentHealth = _maxHealth;
        }
    }
}
