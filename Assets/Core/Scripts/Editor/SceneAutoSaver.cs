using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public class SceneAutoSaver
{
    static SceneAutoSaver()
    {
        EditorApplication.playModeStateChanged += SaveOnPlay;
    }

    private static void SaveOnPlay(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingEditMode)
        {
            EditorSceneManager.SaveOpenScenes();
            AssetDatabase.SaveAssets();
        }
    }
}