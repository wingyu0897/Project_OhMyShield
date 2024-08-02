using UnityEngine;

public class PlayerPresenter : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
	[SerializeField] private GameView _gameView;

	private void Start()
	{
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
