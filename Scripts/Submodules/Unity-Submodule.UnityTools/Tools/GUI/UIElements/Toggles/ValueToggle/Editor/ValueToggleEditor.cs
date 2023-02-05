using UnityEngine;
using System.Collections;
using UnityEditor;

namespace GeekBox.UI.Editor
{
    [CustomEditor(typeof(ValueToggle))]
    public class ValueToggleEditor : TextColorToggleEditor
    {

        #region Fields


        private SerializedProperty valueProp;

        #endregion

        #region Propeties

        #endregion

        #region Methods

        protected override void OnEnable()
        {
            base.OnEnable();

            // Setup the SerializedProperties.
            valueProp = serializedObject.FindProperty("value");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();

            EditorGUILayout.PropertyField(valueProp, new GUIContent("value"));

            // Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
            serializedObject.ApplyModifiedProperties();
        }

        #endregion

        #region Enums



        #endregion
    }
}

