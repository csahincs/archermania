using CSToolbox.Runtime.Attributes;
using Data;
using UnityEngine;

namespace Character
{
    public class HealthComponent : MonoBehaviour
    {
        [Header("Edit Data")]
        [SerializeField] private float _maxHealth;

        private float CurrentHealth
        {
            get => _currentHealth;
            set
            {
                _currentHealth = value;
                _characterRuntimeDebugData.Health = _currentHealth;
            }
        }
    
        [Space, Header("Runtime Data")]
        [SerializeField] private CharacterRuntimeDebugData _characterRuntimeDebugData;

        [SerializeField, ReadOnly] private float _currentHealth;
        
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
