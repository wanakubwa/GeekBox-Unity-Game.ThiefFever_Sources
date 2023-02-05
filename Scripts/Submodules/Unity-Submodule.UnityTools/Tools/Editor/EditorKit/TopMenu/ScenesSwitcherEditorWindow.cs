using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class ScenesSwitcherEditorWindow : EditorWindow
{
    #region Fields

    private const string SCENES_ROOT_PATH = "Content/Scenes/";

    #endregion

    #region Propeties

    private Vector2 ScrollPosition
    {
        get;
        set;
    } = Vector2.zero;

    #endregion

    #region Methods

    [MenuItem("Scenes/Switch scene")]
    public static void OpenWindow()
    {
        ScenesSwitcherEditorWindow window = GetWindow<ScenesSwitcherEditorWindow>("Switch scene");
        window.maxSize = new Vector2(350, 250);
        window.minSize = window.maxSize;
        window.Show();
    }

    void OnGUI()
    {
        ScrollPosition = GUILayout.BeginScrollView(ScrollPosition, false, true, GUILayout.Width(minSize.x));

        string[] scenesPath = GetScenesPaths();
        foreach (string path in scenesPath)
        {
            DrawButton(Path.GetFileNameWithoutExtension(path), () => OpenScene(path));
        }

        GUILayout.EndScrollView();
    }

    private void OpenScene(string path)
    {
        EditorSceneManager.OpenScene(path);
        Close();
    }

    private void DrawButton(string label, Action callback)
    {
        if (GUILayout.Button(label, GUILayout.Height(25)) == true)
        {
            callback();
        }
    }

    private static string[] GetScenesPaths()
    {
        List<string> scenesPaths = new List<string>();

        string scenesRootPath = Path.Combine(Application.dataPath, SCENES_ROOT_PATH);
        scenesPaths.AddRange(Directory.GetFiles(scenesRootPath));

        // Iteracyjne sprawdzenie kolejnych folderow w glownym folderze ze scenami.
        Stack<string[]> directoriesToCheck = new Stack<string[]>();
        directoriesToCheck.Push(Directory.GetDirectories(scenesRootPath));
        while(directoriesToCheck.Count > 0)
        {
            string[] currentDirectories = directoriesToCheck.Pop();
            for(int i =0; i < currentDirectories.Length; i++)
            {
                directoriesToCheck.Push(Directory.GetDirectories(currentDirectories[i]));
                scenesPaths.AddRange(Directory.GetFiles(currentDirectories[i]));
            }
        }

        // Usuwanie pobranych plikow meta.
        scenesPaths.RemoveAll(x => x.Contains(".meta"));

        return scenesPaths.ToArray();
    }

    #endregion

    #region Enums



    #endregion
}
