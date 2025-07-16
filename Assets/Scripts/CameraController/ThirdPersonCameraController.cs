using CSToolbox.Runtime.Attributes;
using UnityEngine;
using Utility.Lerp;

namespace CameraController
{
    public class ThirdPersonCameraController : MonoBehaviour
    {
        [Header("Object References")]
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _target;

        [Header("Data")] 
        [SerializeField] private Vector3 _damping;
        [SerializeField] private Vector3 _offset;
        
        private void Update()
        {
            if (!enabled)
                return;
            
            UpdatePosition();
        }

        [MethodInspectorButton]
        private void UpdatePosition()
        {
            var cameraPosition = _camera.transform.position;
            var cameraTargetPosition = _target.position + _offset;

            var lerpValue = ExponentialDecay.Evaluate(cameraPosition, cameraTargetPosition, 
                _damping, Time.deltaTime);
            
            _camera.transform.position = lerpValue;
            _camera.transform.rotation = Quaternion.LookRotation(_target.forward);
        }

        [MethodInspectorButton]
        private void SetPosition()
        {
            _camera.transform.position = _target.position + _offset;
            _camera.transform.rotation = Quaternion.LookRotation(_target.forward);
        }
    }
}
