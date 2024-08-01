using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Agent
{
	[SerializeField] private AgentDataSO _agentData;

	private float _health;
	public override float Health { get => _health; set => _health = value; }

	public override Agent Target { get; set; }

	public override void Dead()
	{
		GameManager.Instance.poolManager.Push(this);
	}

	public override void PoolInit()
	{
		_health = _agentData.health;
	}
}
