using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuView : UIView
{
	public override void Initialize()
	{
		Bind<Button>("PlayButton");
		Get<Button>("PlayButton").onClick.AddListener(() => GameManager.Instance.gameFlow.ChangeScene(Enums.GAME_STATE.Game));
	}

	private void OnDestroy()
	{
		Get<Button>("PlayButton").onClick.RemoveAllListeners();
	}
}
