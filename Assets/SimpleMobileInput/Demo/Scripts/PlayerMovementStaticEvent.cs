using UnityEngine;

namespace SimpleMobileInput.Demo
{
    public class PlayerMovementStaticEvent : PlayerMovementBase
    {
        [SerializeField]
        private JoystickInputIdentifier _joystickInputIdentifier = JoystickInputIdentifier.None;

        protected override void OnEnable()
        {
            JoystickInputUI.OnDirectionChanged += OnDirectionChanged;
        }

        protected override void OnDisable()
        {
            JoystickInputUI.OnDirectionChanged -= OnDirectionChanged;
        }

        private void OnDirectionChanged(JoystickInputIdentifier joystickInputIdentifier, Vector2 direction)
        {
            if(_joystickInputIdentifier != joystickInputIdentifier) { return; }
            base.Move(direction);
        }
    }
}