using CSToolbox.Runtime.Attributes;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = nameof(CharacterRuntimeData), menuName = "Data/RuntimeData/" + nameof(CharacterRuntimeData))]
    public class CharacterRuntimeData : ScriptableObject
    {
        [Header("Health Data")]
        [field: SerializeField, ReadOnly] public float Health { get; set; }
        
        [Header("Movement Data")]
        [field: SerializeField, ReadOnly] public Vector3 LinearVelocity { get; set; }
        [field: SerializeField, ReadOnly] public Vector3 AngularVelocity { get; set; }

        [Header("Debug Data")] 
        [field: SerializeField] public bool LogMoveInput { get; set; }
        [field: SerializeField] public bool LogLookInput { get; set; }
    }
}
