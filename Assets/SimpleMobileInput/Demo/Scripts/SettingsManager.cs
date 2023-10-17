using UnityEngine;
using UnityEngine.UI;

namespace SimpleMobileInput.Demo
{
    public class SettingsManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _settingsButtonGameObject = null;

        [SerializeField]
        private GameObject _closeSettingsButtonGameObject = null;

        [SerializeField]
        private GameObject _returnToMenuButtonGameObject = null;

        [SerializeField]
        private Text _reverseButtonText = null;

        [SerializeField]
        private GameObject _settingsPanel = null;

        [SerializeField]
        private JoystickInputUI _joystickInputUI = null;

        [SerializeField]
        private ActionInputUI _actionInputUI = null;


        [Header("Joystick Settings Controls")]
        [Space(10)]
        [SerializeField]
        private Slider _joystickButtonLimitSlider = null;

        [SerializeField]
        private Slider _joystickPressedAlphaSlider = null;

        [SerializeField]
        private Toggle _joystickAlwaysVisibleToggle = null;

        [SerializeField]
        private Toggle _joystickFreezeXAxisToggle = null;

        [SerializeField]
        private Toggle _joystickFreezeYAxisToggle = null;

        [SerializeField]
        private Toggle _joystickFixPositionToggle = null;

        [SerializeField]
        private Toggle _joystickButtonFollowTouchToggle = null;

        [SerializeField]
        private Slider _joystickButtonFollowTouchThresholdSlider = null;

        [SerializeField]
        private Toggle _joystickClampToMaxValueToggle = null;

        [SerializeField]
        private Slider _joystickClampToMaxValueSlider = null;

        [Header("Action Button Settings Controls")]
        [Space(10)]
        [SerializeField]
        private Slider _actionButtonPressedAlphaSlider = null;

        [SerializeField]
        private Toggle _actionButtonAlwaysVisibleToggle = null;

        [SerializeField]
        private Toggle _actionButtonFixPositionToggle = null;

        private bool _isReversed = false;

        private float _defaultJoystickButtonLimit;
        private float _defaultPressedAlpha;
        private bool _defaultIsAlwaysVisible;
        private bool _defaultFreezeXAxis;
        private bool _defaultFreezeYAxis;
        private bool _defaultIsFixPosition;
        private bool _defaultFollowPlayerTouch;
        private float _defaultFollowPlayerTouchThreshold;
        private bool _defaultIsClampToMaxValue;
        private float _defaultClampToMaxValueThreshold;

        private bool _defaultActionButtonIsFixPosition;
        private float _defaultActionButtonPressedAlpha;
        private bool _defaultActionButtonIsAlwaysVisible;

        private bool _defaultIsReversed = false;

        void Start()
        {
            HideSettings();
            InitializeDefaultValues();
            InitialiazeJoystickSettings();
            InitialiazeActionButtonSettings();
            UpdateReverseButtonText();
        }

        private void InitializeDefaultValues()
        {
            _defaultJoystickButtonLimit = _joystickInputUI.JoystickButtonLimit;
            _defaultPressedAlpha = _joystickInputUI.PressedAlpha;
            _defaultIsAlwaysVisible = _joystickInputUI.IsAlwaysVisible;
            _defaultFreezeXAxis = _joystickInputUI.FreezeXAxis;
            _defaultFreezeYAxis = _joystickInputUI.FreezeYAxis;
            _defaultIsFixPosition = _joystickInputUI.IsFixPosition;
            _defaultFollowPlayerTouch = _joystickInputUI.FollowPlayerTouch;
            _defaultFollowPlayerTouchThreshold = _joystickInputUI.FollowPlayerTouchThreshold;
            _defaultIsClampToMaxValue = _joystickInputUI.IsClampToMaxValue;
            _defaultClampToMaxValueThreshold = _joystickInputUI.ClampToMaxValueThreshold;

            _defaultActionButtonIsFixPosition = _actionInputUI.IsFixPosition;
            _defaultActionButtonPressedAlpha = _actionInputUI.PressedAlpha;
            _defaultActionButtonIsAlwaysVisible = _actionInputUI.IsAlwaysVisible;
        }

        private void InitialiazeJoystickSettings()
        {
            _joystickButtonLimitSlider.value = _defaultJoystickButtonLimit;
            _joystickPressedAlphaSlider.value = _defaultPressedAlpha;
            _joystickAlwaysVisibleToggle.isOn = _defaultIsAlwaysVisible;
            _joystickFreezeXAxisToggle.isOn = _defaultFreezeXAxis;
            _joystickFreezeYAxisToggle.isOn = _defaultFreezeYAxis;
            _joystickFixPositionToggle.isOn = _defaultIsFixPosition;
            _joystickButtonFollowTouchToggle.isOn = _defaultFollowPlayerTouch;
            _joystickButtonFollowTouchThresholdSlider.value = _defaultFollowPlayerTouchThreshold;
            _joystickClampToMaxValueToggle.isOn = _defaultIsClampToMaxValue;
            _joystickClampToMaxValueSlider.value = _defaultClampToMaxValueThreshold;
            UpdateRestrictions();
        }

        private void InitialiazeActionButtonSettings()
        {
            _actionButtonFixPositionToggle.isOn = _defaultActionButtonIsFixPosition;
            _actionButtonPressedAlphaSlider.value = _defaultActionButtonPressedAlpha;
            _actionButtonAlwaysVisibleToggle.isOn = _defaultActionButtonIsAlwaysVisible;
        }

        private void UpdateRestrictions()
        {
            if (_joystickFixPositionToggle.isOn && _joystickButtonFollowTouchToggle.isOn) 
            {
                _joystickFixPositionToggle.isOn = true;
                _joystickButtonFollowTouchToggle.isOn = false;
                SetJoystickFixPosition(_joystickFixPositionToggle.isOn);
                SetJoystickFollowTouch(_joystickButtonFollowTouchToggle.isOn);
            }

            if (_joystickFixPositionToggle.isOn)
            {
                _joystickButtonFollowTouchToggle.isOn = false;
                SetJoystickFollowTouch(_joystickButtonFollowTouchToggle.isOn);
            }

            _joystickButtonFollowTouchToggle.interactable = !_joystickFixPositionToggle.isOn;
            _joystickFixPositionToggle.interactable = !_joystickButtonFollowTouchToggle.isOn;
            _joystickButtonFollowTouchThresholdSlider.interactable = _joystickButtonFollowTouchToggle.isOn;
            _joystickClampToMaxValueSlider.interactable = _joystickClampToMaxValueToggle.isOn;
        }

        public void ResetToDefault()
        {
            if(_defaultIsReversed != _isReversed) { ReverseSide(); }
            InitialiazeJoystickSettings();
            InitialiazeActionButtonSettings();
        }

        //Joystick Setters
        private void SetJoystickFollowTouch(bool value) { _joystickInputUI.FollowPlayerTouch = value; }
        private void SetJoystickFixPosition(bool value) { _joystickInputUI.IsFixPosition = value; }

        public void ChangeJoystickButtonLimit() { _joystickInputUI.JoystickButtonLimit = _joystickButtonLimitSlider.value; }
        public void ChangeJoystickPressedAlpha() { _joystickInputUI.PressedAlpha = _joystickPressedAlphaSlider.value; }
        public void ChangeJoystickAlwaysVisible() { _joystickInputUI.IsAlwaysVisible = _joystickAlwaysVisibleToggle.isOn; }
        public void ChangeJoystickFreezeXAxis() { _joystickInputUI.FreezeXAxis = _joystickFreezeXAxisToggle.isOn; }
        public void ChangeJoystickFreezeYAxis() { _joystickInputUI.FreezeYAxis = _joystickFreezeYAxisToggle.isOn; }
        public void ChangeJoystickFixPosition() 
        { 
            SetJoystickFixPosition(_joystickFixPositionToggle.isOn);
            UpdateRestrictions();
        }
        public void ChangeJoystickFollowTouch() 
        { 
            SetJoystickFollowTouch(_joystickButtonFollowTouchToggle.isOn);
            UpdateRestrictions();
        }
        public void ChangeJoystickClampToMaxValue() 
        { 
            _joystickInputUI.IsClampToMaxValue = _joystickClampToMaxValueToggle.isOn;
            UpdateRestrictions();
        }
        public void ChangeJoystickFollowTouchThreshold() { _joystickInputUI.FollowPlayerTouchThreshold = _joystickButtonFollowTouchThresholdSlider.value; }
        public void ChangeJoystickClampToMaxValueThreshold() { _joystickInputUI.ClampToMaxValueThreshold = _joystickClampToMaxValueSlider.value; }

        //Action Button Setters
        public void ChangeActionButtonPressedAlpha() { _actionInputUI.PressedAlpha = _actionButtonPressedAlphaSlider.value; }
        public void ChangeActionButtonAlwaysVisible() { _actionInputUI.IsAlwaysVisible = _actionButtonAlwaysVisibleToggle.isOn; }
        public void ChangeActionButtonFixPosition() { _actionInputUI.IsFixPosition = _actionButtonFixPositionToggle.isOn; }

        public void ReverseSide()
        {
            _isReversed = !_isReversed;
            _joystickInputUI.ReverseTouchPanelPositionning();
            _actionInputUI.ReverseTouchPanelPositionning();
            UpdateReverseButtonText();
        }

        public void ShowSettings()
        {
            _settingsButtonGameObject.SetActive(false);
            _closeSettingsButtonGameObject.SetActive(true);
            _returnToMenuButtonGameObject.SetActive(false);
            _settingsPanel.SetActive(true);
        }

        public void HideSettings()
        {
            _settingsButtonGameObject.SetActive(true);
            _closeSettingsButtonGameObject.SetActive(false);
            _returnToMenuButtonGameObject.SetActive(true);
            _settingsPanel.SetActive(false);
        }

        private void UpdateReverseButtonText()
        {
            _reverseButtonText.text = _isReversed ? "(Reversed)".ToUpper() : "(Normal)".ToUpper();
        }
    }
}