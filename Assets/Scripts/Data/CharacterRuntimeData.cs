using CSToolbox.Runtime.Attributes;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = nameof(CharacterRuntimeData), menuName = "Data/RuntimeData/" + nameof(CharacterRuntimeData))]
    public class CharacterRuntimeData : ScriptableObject
    {
        [Header("Health Data")] 
        [SerializeField, ReadOnly] public float Health;

        [Header("Movement Data")] 
        [SerializeField, ReadOnly] public Vector3 LinearVelocity;
        [SerializeField, ReadOnly] public Vector3 AngularVelocity;

        [Header("Debug Data")] 
        [field: SerializeField] public bool LogMoveInput { get; set; }
        [field: SerializeField] public bool LogLookInput { get; set; }
        [field: SerializeField] public bool UseExponentialDecay { get; set; }
    }
}
