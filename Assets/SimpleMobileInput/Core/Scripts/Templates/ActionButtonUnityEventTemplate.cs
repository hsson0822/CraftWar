using UnityEngine;

namespace SimpleMobileInput
{
    public class ActionButtonUnityEventTemplate : MonoBehaviour
    {
        public void OnButtonDown()
        {
            Debug.Log("Button pressed.");
        }

        public void OnButtonUp()
        {
            Debug.Log("Button Released.");
        }
    }
}