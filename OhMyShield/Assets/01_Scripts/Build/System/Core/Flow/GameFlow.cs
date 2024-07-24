using System;
using UnityEngine.SceneManagement;
using Enums;


public class GameFlow
{
    public event Action OnChangeScene;

    public void ChangeScene(GAME_STATE scene)
	{
        OnChangeScene?.Invoke();

        string sceneName = scene.ToString();
        SceneManager.LoadScene(sceneName);
	}
}
