using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

	private Shield _shield;
	private Player _player;

	private void Awake()
	{
		_shield = transform.Find("Shield").GetComponent<Shield>();
		_player = GetComponent<Player>();
	}

	public void JoystickValueToShield(Vector2 value)
	{
		float angle = Mathf.Atan2(value.y, value.x) * Mathf.Rad2Deg;
		_shield.SetDirection(angle);
	}

	public void DoAttack()
	{
		_player.Attack();
	}
}
