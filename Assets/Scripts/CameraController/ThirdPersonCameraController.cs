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
        [SerializeField, Range(0, 10f)] private float _distance;
        
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

            var lerpValue = ExponentialDecay.Evaluate(cameraPosition, CalculatePosition(), 
                _damping, Time.deltaTime);
            
            _camera.transform.position = lerpValue;
            _camera.transform.rotation = Quaternion.LookRotation(_target.forward);
        }

        [MethodInspectorButton]
        private void SetPosition()
        {
            _camera.transform.position = CalculatePosition();
            _camera.transform.rotation = Quaternion.LookRotation(_target.forward);
        }

        private Vector3 CalculatePosition()
        {
            return _target.transform.position - _target.forward.normalized * _distance + _offset;
        }
    }
}
