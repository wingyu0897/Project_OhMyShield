using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseView : UIView
{
	public override void Initialize()
	{
		Bind<Button>("ContinueButton");
		Bind<Button>("MenuButton");


		Get<Button>("ContinueButton").onClick.AddListener(() =>
		{
			Time.timeScale = 1f;
			UIViewManager.ShowView<GameView>();
		});

		Get<Button>("MenuButton").onClick.AddListener(() => GameManager.Instance.ChangeScene(Enums.GAME_STATE.Menu));
	}

	private void OnDestroy()
	{
		foreach (Button btn in _uiElements[typeof(Button)])
		{
			btn.onClick.RemoveAllListeners();
		}
	}
}
