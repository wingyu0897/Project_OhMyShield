using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraSizeFitter : MonoBehaviour
{
	[SerializeField] private Vector3 _camBound;

	private void Awake()
	{
		FitCameraToBound();
	}

	private void FitCameraToBound()
	{
		Camera cam = GetComponent<Camera>();

		float boundAspect = _camBound.x / _camBound.y;
		
		if (cam.aspect > boundAspect)
		{
			cam.orthographicSize = _camBound.y * 0.5f;
		}
		else
		{
			cam.orthographicSize = _camBound.x / cam.aspect * 0.5f;
		}
	}

#if UNITY_EDITOR
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(transform.position, _camBound);
		Gizmos.color = Color.white;
	}
#endif
}
