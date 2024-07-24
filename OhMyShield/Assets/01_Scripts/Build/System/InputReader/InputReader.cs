using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "InputReader")]
public class InputReader : ScriptableObject, InputActions.IPlayerActionsActions
{
	public event Action<InputAction.CallbackContext> OnTouched;

	private InputActions _inputActions;

	private void OnEnable()
	{
		if (_inputActions is null)
		{
			_inputActions = new InputActions();
			_inputActions.PlayerActions.AddCallbacks(this);
		}

		_inputActions.PlayerActions.Enable();
	}

	private void OnDisable()
	{
		if (_inputActions != null)
		{
			_inputActions.PlayerActions.Disable();
		}
	}

	public void OnTouch(InputAction.CallbackContext context)
	{
		OnTouched?.Invoke(context);
	}
}
