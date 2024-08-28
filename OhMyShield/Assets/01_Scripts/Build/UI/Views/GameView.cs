using UnityEngine;
using UnityEngine.UI;

public class GameView : UIView
{
	public override void Initialize()
	{
		Bind<Button>("AttackButton");
		Bind<Joystick>("JoystickTouchArea");
		Bind<Button>("PauseButton");

		Get<Button>("PauseButton").onClick.AddListener(() =>
		{
			Time.timeScale = 0;
			UIViewManager.ShowView<GamePauseView>();
		});
	}

	public void SetUp(PlayerPresenter presenter)
	{
		Get<Joystick>("JoystickTouchArea").OnValueChanged.AddListener(presenter.OnJoystickValueChanged);
		Get<Button>("AttackButton").onClick.AddListener(presenter.OnAttackButtonClickHandler);
	}

	private void OnDestroy()
	{
		Get<Joystick>("JoystickTouchArea").OnValueChanged.RemoveAllListeners();
		Get<Button>("AttackButton").onClick.RemoveAllListeners();
		Get<Button>("PauseButton").onClick.RemoveAllListeners();
	}
}
