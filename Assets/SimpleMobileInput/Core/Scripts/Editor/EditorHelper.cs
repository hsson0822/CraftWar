using UnityEditor;
using UnityEngine;

namespace SimpleMobileInput.Editor
{
    public static class EditorHelper
    {
        private const float HEADER_SPACING = 20f;
        private const float PROPERTY_SPACING = 10f;

        public static void Header(string title)
        {
            GUILayout.Space(HEADER_SPACING);
            EditorGUILayout.LabelField(title, EditorStyles.boldLabel);
        }

        public static void PropertySpacing()
        {
            GUILayout.Space(PROPERTY_SPACING);
        }

        public static void Validations(MobileInputUIBase mobileInputUIBase)
        {
            if (!mobileInputUIBase.IsCanvasCameraRenderModeValid()) { EditorGUILayout.HelpBox("You will need the render mode \"ScreenSpaceCamera\" on your canvas.", MessageType.Error, true); }
            if (!mobileInputUIBase.IsCameraValid()) { EditorGUILayout.HelpBox("You will need a orthographic camera on your canvas.", MessageType.Error, true); }
        }
    }
}