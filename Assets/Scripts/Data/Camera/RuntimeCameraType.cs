using UnityEngine;

namespace Data.Camera
{
    [CreateAssetMenu(fileName = nameof(RuntimeCameraType), menuName = "Data/Camera/" + nameof(RuntimeCameraType))]   
    public class RuntimeCameraType : ScriptableObject
    {
        [field: SerializeField] public CameraType Type { get; private set; }
    }
}
