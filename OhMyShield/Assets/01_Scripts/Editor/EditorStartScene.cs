using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public static class EditorStartScene
{
    static EditorStartScene()
    {
        string scenePath = EditorBuildSettings.scenes[0].path;
        SceneAsset sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath);

        if (sceneAsset)
            EditorSceneManager.playModeStartScene = sceneAsset;

        EditorApplication.playModeStateChanged += LoadDefaultScene;
    }

    private static void LoadDefaultScene(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingEditMode)
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        }
    }
}