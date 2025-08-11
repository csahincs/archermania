using Data;
using UnityEngine;
using UnityEngine.Serialization;

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
                _characterRuntimeDebugData.Health = _currentHealth;
            }
        }
    
        [FormerlySerializedAs("_characterRuntimeData")]
        [Space, Header("Runtime Data")]
        [SerializeField] private CharacterRuntimeDebugData _characterRuntimeDebugData;

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
