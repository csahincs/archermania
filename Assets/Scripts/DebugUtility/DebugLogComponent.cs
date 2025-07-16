using UnityEngine;

namespace DebugUtility
{
    public class DebugLogComponent : MonoBehaviour
    {
        public void LogError(string message)
        {
            Debug.LogError(message, gameObject);
        }
    }
}
