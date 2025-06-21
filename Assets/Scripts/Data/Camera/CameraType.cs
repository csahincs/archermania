using UnityEngine;

namespace Data.Camera
{
    [CreateAssetMenu(fileName = nameof(CameraType), menuName = "Data/Camera/" + nameof(CameraType))]
    public class CameraType : ScriptableObject
    {
        [field: SerializeField] public string Type { get; private set; }

        private void OnValidate()
        {
            if(name != Type)
                name = Type;
        }
    }
}
