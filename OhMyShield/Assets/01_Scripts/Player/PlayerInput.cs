using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
	[SerializeField] private Joystick _joystick;

	private Shield _shield;

	private void Awake()
	{
		_shield = transform.Find("Shield").GetComponent<Shield>();
	}

	private void OnEnable()
	{
		_inputReader.OnTouched += OnTouched;
		_joystick.OnValueChanged.AddListener(JoystickValueToShield);
	}

	private void OnDisable()
	{
		_inputReader.OnTouched -= OnTouched;
	}

	private void JoystickValueToShield(Vector2 value)
	{
		float angle = Mathf.Atan2(value.y, value.x) * Mathf.Rad2Deg;
		_shield.SetDirection(angle);
	}

	public void OnTouched(InputAction.CallbackContext context)
	{
		
	}
}
