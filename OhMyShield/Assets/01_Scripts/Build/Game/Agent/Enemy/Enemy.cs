using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Agent, IHealth
{
	[SerializeField] private AgentDataSO _agentData;

	private float _health;
	public float Health { get => _health; set => _health = value; }

	public override Agent Target { get; set; }

	public void ModifyHealth(float change)
	{
		_health += change;
		_health = Mathf.Clamp(_health, 0, _agentData.health);
	}

	public override void PoolInit()
	{
		_health = _agentData.health;
	}
}
