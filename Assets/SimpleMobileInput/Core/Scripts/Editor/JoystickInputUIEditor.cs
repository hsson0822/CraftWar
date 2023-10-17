using UnityEditor;
using UnityEngine;

namespace SimpleMobileInput.Editor
{
    [CustomEditor(typeof(JoystickInputUI))]
    public class JoystickInputUIEditor : UnityEditor.Editor
    {
        SerializedProperty _joystickInputIdentifier;
        SerializedProperty _cam;
        SerializedProperty _parentCanvas;
        SerializedProperty _canvasGroup;
        SerializedProperty _touchPanelGameObject;
        SerializedProperty _isFixPosition;
        SerializedProperty _isAlwaysVisible;
        SerializedProperty _eventType;
        SerializedProperty _pressedAlpha;

        SerializedProperty _joystickButtonGameObject;
        SerializedProperty _joystickBackgroundGameObject;

        SerializedProperty _joystickButtonLimit;
        SerializedProperty _isFollowPlayerTouch;
        SerializedProperty _followPlayerTouchThreshold;
        SerializedProperty _freezeXAxis;
        SerializedProperty _freezeYAxis;
        SerializedProperty _isClampToMaxValue;
        SerializedProperty _clampToMaxValueThreshold;

        SerializedProperty _mobileJoystickChangeDirectionEvent;

        SerializedProperty _isGizmosVisible;
        SerializedProperty _repositioningThresholdGizmoColor;
        SerializedProperty _joystickButtonLimitGizmoColor;
        SerializedProperty _targetPositionGizmoColor;
        SerializedProperty _touchZoneLimitGizmoColor;

        JoystickInputUI _joystickInputUI = null;

        void OnEnable()
        {
            InitializeSerializedProperties();
            _joystickInputUI = (JoystickInputUI)target;
            _joystickInputUI.GetInitialComponents();
        }

        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();

            serializedObject.Update();

            Validations();

            ScriptGroup();
            IdentifierGroup();
            ComponentsGroup();
            VisualGroup();
            AdjustmentsGroup();
            PositioningGroup();
            EventsGroup();
            GizmosGroup();
            CoreComponentsGroup();

            serializedObject.ApplyModifiedProperties();
        }

        private void InitializeSerializedProperties()
        {
            _joystickInputIdentifier = serializedObject.FindProperty("_joystickInputIdentifier");
            _cam = serializedObject.FindProperty("_cam");
            _parentCanvas = serializedObject.FindProperty("_parentCanvas");
            _canvasGroup = serializedObject.FindProperty("_canvasGroup");
            _touchPanelGameObject = serializedObject.FindProperty("_touchPanelGameObject");

            _isFixPosition = serializedObject.FindProperty("_isFixPosition");
            _isAlwaysVisible = serializedObject.FindProperty("_isAlwaysVisible");

            _pressedAlpha = serializedObject.FindProperty("_pressedAlpha");

            _eventType = serializedObject.FindProperty("_eventType");

            _joystickButtonGameObject = serializedObject.FindProperty("_joystickButtonGameObject");
            _joystickBackgroundGameObject = serializedObject.FindProperty("_joystickBackgroundGameObject");

            _isFollowPlayerTouch = serializedObject.FindProperty("_isFollowPlayerTouch");
            _joystickButtonLimit = serializedObject.FindProperty("_joystickButtonLimit");
            _followPlayerTouchThreshold = serializedObject.FindProperty("_followPlayerTouchThreshold");

            _freezeXAxis = serializedObject.FindProperty("_freezeXAxis");
            _freezeYAxis = serializedObject.FindProperty("_freezeYAxis");

            _isClampToMaxValue = serializedObject.FindProperty("_isClampToMaxValue");
            _clampToMaxValueThreshold = serializedObject.FindProperty("_clampToMaxValueThreshold");

            _mobileJoystickChangeDirectionEvent = serializedObject.FindProperty("_mobileJoystickChangeDirectionEvent");

            _isGizmosVisible = serializedObject.FindProperty("_isGizmosVisible");
            _repositioningThresholdGizmoColor = serializedObject.FindProperty("_repositioningThresholdGizmoColor");
            _joystickButtonLimitGizmoColor = serializedObject.FindProperty("_joystickButtonLimitGizmoColor");
            _targetPositionGizmoColor = serializedObject.FindProperty("_targetPositionGizmoColor");
            _touchZoneLimitGizmoColor = serializedObject.FindProperty("_touchZoneLimitGizmoColor");
        }

        private void ScriptGroup()
        {
            GUI.enabled = false;
            EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour((JoystickInputUI)target), typeof(JoystickInputUI), false);
            GUI.enabled = true;
        }

        private void IdentifierGroup()
        {
            EditorHelper.Header("Identifier");
            EditorGUILayout.PropertyField(_joystickInputIdentifier, new GUIContent("Input Identifier", "Unique identifier of the input."));
        }

        private void ComponentsGroup()
        {
            EditorHelper.Header("Components");
            EditorGUILayout.PropertyField(_joystickButtonGameObject, new GUIContent("Button"));
            EditorGUILayout.PropertyField(_joystickBackgroundGameObject, new GUIContent("Button Background"));
        }

        private void VisualGroup()
        {
            EditorHelper.Header("Visual");
            EditorGUILayout.HelpBox("Set the alpha of the control when you touch the screen.", MessageType.Info, true);
            _pressedAlpha.floatValue = EditorGUILayout.Slider("Pressed Alpha", _pressedAlpha.floatValue, 0f, 1f);
            EditorGUILayout.PropertyField(_isAlwaysVisible, new GUIContent("Always Visible", "Keep the control visible even when you don't touch the screen."));
        }

        private void AdjustmentsGroup()
        {
            EditorHelper.Header("Adjustments");
            EditorGUILayout.HelpBox("Set the limit distance that the center button can travel.", MessageType.Info, true);
            _joystickButtonLimit.floatValue = EditorGUILayout.Slider("Button Limit Distance", _joystickButtonLimit.floatValue, 0f, 10f);

            EditorHelper.PropertySpacing();
            EditorGUILayout.PropertyField(_freezeXAxis, new GUIContent("Freeze X Axis", "Limit the movement of the X axis."));
            EditorGUILayout.PropertyField(_freezeYAxis, new GUIContent("Freeze Y Axis", "Limit the movement of the Y axis."));
            EditorHelper.PropertySpacing();

            EditorGUILayout.PropertyField(_isClampToMaxValue, new GUIContent("Always Clamp To Max Value", "Determine if we want to always clamp to max value."));
            if (_isClampToMaxValue.boolValue)
            {
                EditorGUILayout.HelpBox("Set the threshold before we apply the clamp to max value.", MessageType.Info, true);
                _clampToMaxValueThreshold.floatValue = EditorGUILayout.Slider("Clamp Value Threshold", _clampToMaxValueThreshold.floatValue, 0f, 1f);
            }
        }

        private void PositioningGroup()
        {
            EditorHelper.Header("Positioning");

            //Prevent serialization error of 2 boolean saved.
            if (_isFixPosition.boolValue && _isFollowPlayerTouch.boolValue) 
            { 
                _isFixPosition.boolValue = true;
                _isFollowPlayerTouch.boolValue = false;
            }

            //Fix position
            EditorGUI.BeginDisabledGroup(_isFollowPlayerTouch.boolValue);
            EditorGUILayout.PropertyField(_isFixPosition, new GUIContent("Fix Position", "Keep the control to his origin position."));
            EditorGUI.EndDisabledGroup();

            if (_isFixPosition.boolValue) { _isFollowPlayerTouch.boolValue = false; }

            //Follow touch
            EditorGUI.BeginDisabledGroup(_isFixPosition.boolValue);
            EditorGUILayout.PropertyField(_isFollowPlayerTouch, new GUIContent("Follow Touch", "Determine if the joystick need to follow where we touch the screen."));
            EditorGUI.EndDisabledGroup();

            if (_isFollowPlayerTouch.boolValue)
            {
                _isFixPosition.boolValue = false;
                EditorGUILayout.HelpBox("Set the threshold before we move the joystick on the screen.", MessageType.Info, true);
                _followPlayerTouchThreshold.floatValue = EditorGUILayout.Slider("Following Threshold", _followPlayerTouchThreshold.floatValue, 0f, 10f);
            }

            if (GUILayout.Button("Reverse Side"))
            {
                _joystickInputUI.ReverseTouchPanelPositionning();
            }
        }

        private void EventsGroup()
        {
            EditorHelper.Header("Events");
            EditorGUILayout.PropertyField(_eventType, new GUIContent("Event Type To Send", "The type of events you want to send."));
            EditorGUILayout.PropertyField(_mobileJoystickChangeDirectionEvent, new GUIContent("On Direction Changed"));
        }

        private void GizmosGroup()
        {
            EditorHelper.Header("Gizmos");
            EditorGUILayout.PropertyField(_isGizmosVisible, new GUIContent("Show Gizmos", "Show the gizmos in the Unity's editor window."));
            EditorGUILayout.PropertyField(_joystickButtonLimitGizmoColor, new GUIContent("Joystick Button Limit Gizmo Color"));
            EditorGUILayout.PropertyField(_repositioningThresholdGizmoColor, new GUIContent("Repositioning Threshold Gizmo Color"));
            EditorGUILayout.PropertyField(_targetPositionGizmoColor, new GUIContent("Target Position Gizmo Color"));
            EditorGUILayout.PropertyField(_touchZoneLimitGizmoColor, new GUIContent("Touch Zone Limit Gizmo Color"));
        }

        private void CoreComponentsGroup()
        {
            EditorHelper.Header("Core Components");
            EditorGUILayout.PropertyField(_cam, new GUIContent("Camera"));
            EditorGUILayout.PropertyField(_parentCanvas, new GUIContent("Canvas"));
            EditorGUILayout.PropertyField(_canvasGroup, new GUIContent("Canvas Group"));
            EditorGUILayout.PropertyField(_touchPanelGameObject, new GUIContent("Touch Panel"));
        }

        private void Validations()
        {
            EditorHelper.Validations(_joystickInputUI);
            if (Helper.IsStaticEventAllowed(_joystickInputUI.EventType) && _joystickInputUI.JoystickInputIdentifier == JoystickInputIdentifier.None)
            {
                EditorGUILayout.HelpBox("You will need an input identifier if you use the static event.", MessageType.Warning, true);
            }
        }
    }
}