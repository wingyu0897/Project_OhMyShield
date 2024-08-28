using UnityEngine;

public class PlayerPresenter : MonoBehaviour
{
    private PlayerInput _playerInput;
	private GameView _gameView;

	private void Start()
	{
		_playerInput = BattleManager.Instance.Player.GetComponent<PlayerInput>();
		_gameView = UIViewManager.GetView<GameView>() as GameView;
		_gameView?.SetUp(this);
	}

	public void OnAttackButtonClickHandler()
	{
		_playerInput?.DoAttack();
	}

	public void OnJoystickValueChanged(Vector2 value)
	{
		_playerInput?.JoystickValueToShield(value);
	}
}
