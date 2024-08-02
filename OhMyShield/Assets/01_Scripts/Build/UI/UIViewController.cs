using Enums;
using UnityEngine;

public class UIViewController : MonoBehaviour, ISystemComponent
{
	public void OnSceneChanged(GAME_STATE state)
	{
		IntializeSceneUI();
	}

	private void IntializeSceneUI()
	{
		FindObjectOfType<UIViewManager>();
	}
}
