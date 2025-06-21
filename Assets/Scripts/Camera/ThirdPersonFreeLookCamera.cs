using Data.Camera;
using UnityEngine;
using CameraType = Data.Camera.CameraType;

namespace Camera
{
    public class ThirdPersonFreeLookCamera : MonoBehaviour
    {
        [Header("Data References")]
        [SerializeField] private RuntimeCameraType _runtimeCameraType;
        [SerializeField] private CameraType _cameraType;
    
        [Header("Object References")]
        [SerializeField] private UnityEngine.Camera _camera;
        [SerializeField] private Transform _target;
    
        [Header("Data")]
        [SerializeField] private float _verticalOffset;
        [SerializeField] private float _horizontalOffset;
        
        private void Update()
        {
            if (!enabled || _runtimeCameraType.Type != _cameraType)
                return;
            
            _camera.transform.position = 
                _target.transform.position + 
                (Vector3.up * _verticalOffset) - 
                (_target.transform.forward * _horizontalOffset);
            
            _camera.transform.LookAt(_target.transform);
        }
    }
}