using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = nameof(CharacterRuntimeData), menuName = "Data/" + nameof(CharacterRuntimeData))]
    public class CharacterRuntimeData : ScriptableObject
    {
        [Header("Velocity Data")]
        public Vector3 LinearVelocity;
        public Vector3 AngularVelocity;

        [Header("Debug Data")] 
        public bool LogMoveInput;
        public bool LogLookInput;

    }
}
