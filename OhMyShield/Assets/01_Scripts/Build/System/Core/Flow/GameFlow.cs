using System;
using UnityEngine.SceneManagement;
using Enums;


public class GameFlow
{
    public event Action<GAME_STATE> OnSceneChanged;

    public void ChangeScene(GAME_STATE scene)
	{
        string sceneName = scene.ToString();
        SceneManager.LoadScene(sceneName);

        OnSceneChanged?.Invoke(scene);
	}
}
