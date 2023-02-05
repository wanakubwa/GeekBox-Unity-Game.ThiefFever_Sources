using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(LocalizedTMPro))]
public class LocalizedTMProEditor :
#if UNITY_2021_2_OR_NEWER
    TMPro.EditorUtilities.TMP_EditorPanelUI
#else
    TMPro.EditorUtilities.TMP_EditorPanel
#endif

{
#region Fields


#endregion

#region Propeties



#endregion

#region Methods

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        LocalizedTMPro localizedTMPro = (LocalizedTMPro)target;
        localizedTMPro.LocalizedKey = EditorGUILayout.TextField("Localized Key", localizedTMPro.LocalizedKey);

        // Wykorzystane do zapisania zmian z edytora. Bez wychwycenia czy nastapily zmiany nie zostana one zapisane do propertki.
        if (GUI.changed == true)
        {
            EditorUtility.SetDirty(localizedTMPro);
            EditorSceneManager.MarkSceneDirty(localizedTMPro.gameObject.scene);
        }
    }

#endregion

#region Handlers



#endregion
}
