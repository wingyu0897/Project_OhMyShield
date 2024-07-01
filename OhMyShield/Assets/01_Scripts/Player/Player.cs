using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

	private void OnEnable()
	{
		_inputReader.OnTouched += OnTouched;
	}

	private void OnDisable()
	{
		_inputReader.OnTouched -= OnTouched;
	}

	public void OnTouched(InputAction.CallbackContext context)
	{
		print(context.phase.ToString());
	}
}
