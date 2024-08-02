using UnityEngine.UI;

public class GameView : UIView
{
	public override void Initialize()
	{
		Bind<Button>("AttackButton");
		Bind<Joystick>("JoystickTouchArea");
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
	}
}
