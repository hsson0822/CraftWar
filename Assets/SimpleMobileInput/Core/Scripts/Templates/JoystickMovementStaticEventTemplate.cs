using UnityEngine;

namespace SimpleMobileInput
{
    public class JoystickMovementStaticEventTemplate : MonoBehaviour
    {
        [SerializeField]
        private JoystickInputIdentifier _joystickInputIdentifier = JoystickInputIdentifier.None;

        private void OnEnable()
        {
            JoystickInputUI.OnDirectionChanged += OnDirectionChanged;
        }

        private void OnDisable()
        {
            JoystickInputUI.OnDirectionChanged -= OnDirectionChanged;
        }

        private void OnDirectionChanged(JoystickInputIdentifier joystickInputIdentifier, Vector2 direction)
        {
            if (_joystickInputIdentifier != joystickInputIdentifier) { return; }
            Debug.Log(string.Format("{0} - OnDirectionChanged : {1}", joystickInputIdentifier.ToString(), direction));
        }
    }
}