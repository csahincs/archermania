using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = nameof(CharacterRuntimeData), menuName = "Data/RuntimeData/" + nameof(CharacterRuntimeData))]
    public class CharacterRuntimeData : ScriptableObject
    {
        [Header("Health Data")]
        public float Health;
        
        [Header("Movement Data")]
        public Vector3 LinearVelocity;
        public Vector3 AngularVelocity;

        [Header("Debug Data")] 
        public bool LogMoveInput;
        public bool LogLookInput;
    }
}
