using UnityEngine;

namespace Utility.Lerp
{
    public static class ExponentialDecay
    {
        public static float Evaluate(float a, float b, float decay, float delta)
        {
            return b + (a - b) * Mathf.Exp(-decay * delta);
        }
        
        public static Vector3 Evaluate(Vector3 a, Vector3 b, Vector3 decay, float delta)
        {
            var x = Evaluate(a.x, b.x, decay.x, delta);
            var y = Evaluate(a.y, b.y, decay.y, delta);
            var z = Evaluate(a.z, b.z, decay.z, delta);
            
            return new Vector3(x, y, z);
        }
        
        public static Vector3 Evaluate(Vector3 a, Vector3 b, float decay, float delta)
        {
            return b + (a - b) * Mathf.Exp(-decay * delta);
        }
    }
}
