using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
	[SerializeField] private Joystick _joystick;

	private Shield _shield;
	private Player _player;

	private bool _isOverUI = false;

	private void Awake()
	{
		_shield = transform.Find("Shield").GetComponent<Shield>();
		_player = GetComponent<Player>();
	}

	private void Start()
	{
		//UIViewManager.Instance.
	}

	private void Update()
	{
		_isOverUI = EventSystem.current.IsPointerOverGameObject();
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
