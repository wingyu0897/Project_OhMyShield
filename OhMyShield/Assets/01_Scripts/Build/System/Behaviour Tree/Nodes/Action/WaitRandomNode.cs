using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitRandomNode : ActionNode
{
    public float minValue;
    public float maxValue;

    private float _duration;
    private float _startTime;

	protected override void OnStart()
	{
		_startTime = Time.time;
		_duration = Random.Range(minValue, maxValue);
	}

	protected override void OnStop()
	{

	}

	protected override State OnUpdate()
	{
		if (Time.time - _startTime > _duration)
		{
			return State.Success;
		}

		return State.Running;
	}
}
