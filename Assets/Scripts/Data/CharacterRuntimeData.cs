using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = nameof(CharacterRuntimeData), menuName = "Data/RuntimeData/" + nameof(CharacterRuntimeData))]
    public class CharacterRuntimeData : ScriptableObject
    {
        [Header("Move Data")]
        [field: SerializeField] public float MoveSpeed { get; set; }
        [field: SerializeField] public float MoveDecay { get; set; }
        
        [Header("Rotation Data")]
        [field: SerializeField] public float RotationSpeed { get; set; }
        [field: SerializeField] public float RotationDecay { get; set; }
        
        [Header("Jump Data")]
        [field: SerializeField] public int JumpCount { get; set; }
        [field: SerializeField] public float JumpForce { get; set; }
        [field: SerializeField] public float JumpCooldown { get; set; }
        [field: SerializeField] public float GroundCheckDistance { get; set; }
    }
}
