using UnityEngine;
using UnityEditor;

namespace SimpleMobileInput.Editor
{
    [CustomEditor(typeof(ActionInputUI))]
    public class ActionInputUIEditor : UnityEditor.Editor
    {
        SerializedProperty _actionInputIdentifier;
        SerializedProperty _cam;
        SerializedProperty _parentCanvas;
        SerializedProperty _canvasGroup;
        SerializedProperty _touchPanelGameObject;
        SerializedProperty _isFixPosition;
        SerializedProperty _isAlwaysVisible;
        SerializedProperty _eventType;
        SerializedProperty _pressedAlpha;

        SerializedProperty _buttonGameObject;

        SerializedProperty _mobileActionDownEvent;
        SerializedProperty _mobileActionUpEvent;

        SerializedProperty _isGizmosVisible;
        SerializedProperty _targetPositionGizmoColor;
        SerializedProperty _touchZoneLimitGizmoColor;

        private ActionInputUI _actionInputUI = null;

        void OnEnable()
        {
            InitializeSerializedProperties();
            _actionInputUI = (ActionInputUI)target;
            _actionInputUI.GetInitialComponents();
        }

        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();

            serializedObject.Update();

            Validations();

            ScriptGroup();
            IdentifierGroup();
            ComponentsGroup();
            VisualsGroup();
            PositionningGroup();
            EventsGroup();
            GizmosGroup();
            CoreComponentsGroup();

            serializedObject.ApplyModifiedProperties();
        }

        private void InitializeSerializedProperties()
        {
            _actionInputIdentifier = serializedObject.FindProperty("_actionInputIdentifier");
            _cam = serializedObject.FindProperty("_cam");
            _parentCanvas = serializedObject.FindProperty("_parentCanvas");
            _canvasGroup = serializedObject.FindProperty("_canvasGroup");
            _touchPanelGameObject = serializedObject.FindProperty("_touchPanelGameObject");

            _isFixPosition = serializedObject.FindProperty("_isFixPosition");
            _isAlwaysVisible = serializedObject.FindProperty("_isAlwaysVisible");

            _pressedAlpha = serializedObject.FindProperty("_pressedAlpha");

            _eventType = serializedObject.FindProperty("_eventType");

            _buttonGameObject = serializedObject.FindProperty("_buttonGameObject");
            _mobileActionDownEvent = serializedObject.FindProperty("_mobileActionDownEvent");
            _mobileActionUpEvent = serializedObject.FindProperty("_mobileActionUpEvent");

            _isGizmosVisible = serializedObject.FindProperty("_isGizmosVisible");
            _targetPositionGizmoColor = serializedObject.FindProperty("_targetPositionGizmoColor");
            _touchZoneLimitGizmoColor = serializedObject.FindProperty("_touchZoneLimitGizmoColor");
        }

        private void ScriptGroup()
        {
            GUI.enabled = false;
            EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour((ActionInputUI)target), typeof(ActionInputUI), false);
            GUI.enabled = true;
        }

        private void IdentifierGroup()
        {
            EditorHelper.Header("Identifier");
            EditorGUILayout.PropertyField(_actionInputIdentifier, new GUIContent("Input Identifier", "Unique identifier of the input."));
        }

        private void ComponentsGroup()
        {
            EditorHelper.Header("Components");
            EditorGUILayout.PropertyField(_buttonGameObject, new GUIContent("Button"));
        }

        private void VisualsGroup()
        {
            EditorHelper.Header("Visual");

            EditorGUILayout.HelpBox("Set the alpha of the control when you touch the screen.", MessageType.Info, true);
            _pressedAlpha.floatValue = EditorGUILayout.Slider("Pressed alpha", _pressedAlpha.floatValue, 0f, 1f);
            EditorGUILayout.PropertyField(_isAlwaysVisible, new GUIContent("Always Visible", "Keep the control visible even when you don't touch the screen."));
        }

        private void PositionningGroup()
        {
            EditorHelper.Header("Positioning");
            EditorGUILayout.PropertyField(_isFixPosition, new GUIContent("Fix Position", "Keep the control to his origin position."));
            if (GUILayout.Button("Reverse Side"))
            {
                _actionInputUI.ReverseTouchPanelPositionning();
            }
        }

        private void EventsGroup()
        {
            EditorHelper.Header("Events");
            EditorGUILayout.PropertyField(_eventType, new GUIContent("Event Type To Send", "The type of events you want to send."));
            EditorGUILayout.PropertyField(_mobileActionDownEvent, new GUIContent("On Button Down"));
            EditorGUILayout.PropertyField(_mobileActionUpEvent, new GUIContent("On Button Up"));
        }

        private void GizmosGroup()
        {
            EditorHelper.Header("Gizmos");
            EditorGUILayout.PropertyField(_isGizmosVisible, new GUIContent("Show Gizmos", "Show the gizmos in the Unity's editor window."));
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
            EditorHelper.Validations(_actionInputUI);
            if (Helper.IsStaticEventAllowed(_actionInputUI.EventType) && _actionInputUI.ActionInputIdentifier == ActionInputIdentifier.None)
            {
                EditorGUILayout.HelpBox("You will need an input identifier if you use the static event.", MessageType.Warning, true);
            }
        }
    }
}