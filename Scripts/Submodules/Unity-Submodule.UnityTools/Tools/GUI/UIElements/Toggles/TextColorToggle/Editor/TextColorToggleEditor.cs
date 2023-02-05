using UnityEngine;
using UnityEditor;

namespace GeekBox.UI.Editor
{
    [CustomEditor(typeof(TextColorToggle))]
    public class TextColorToggleEditor : UnityEditor.UI.ToggleEditor
    {

        #region Fields

        private SerializedProperty targetImageProp;
        private SerializedProperty activeSpriteProp;
        private SerializedProperty inactiveSpriteProp;

        private SerializedProperty textLabelProp;
        private SerializedProperty activeTextColorProp;
        private SerializedProperty inactiveTextColorProp;

        #endregion

        #region Propeties

        #endregion

        #region Methods

        protected override void OnEnable()
        {
            base.OnEnable();

            // Setup the SerializedProperties.
            textLabelProp = serializedObject.FindProperty("labelText");
            activeTextColorProp = serializedObject.FindProperty("activeTextColor");
            inactiveTextColorProp = serializedObject.FindProperty("inactiveTextColor");
            targetImageProp = serializedObject.FindProperty("targetImage");
            activeSpriteProp = serializedObject.FindProperty("activeSprite");
            inactiveSpriteProp = serializedObject.FindProperty("inactiveSprite");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();

            EditorGUILayout.PropertyField(textLabelProp, new GUIContent("TMP Label Text"));
            EditorGUILayout.PropertyField(activeTextColorProp, new GUIContent("Active text color"));
            EditorGUILayout.PropertyField(inactiveTextColorProp, new GUIContent("Inactive text color"));
            EditorGUILayout.PropertyField(targetImageProp, new GUIContent("Target image"));
            EditorGUILayout.PropertyField(activeSpriteProp, new GUIContent("Active Sprite"));
            EditorGUILayout.PropertyField(inactiveSpriteProp, new GUIContent("Inactive Sprite"));

            // Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
            serializedObject.ApplyModifiedProperties();
        }

        #endregion

        #region Enums



        #endregion
    }
}

