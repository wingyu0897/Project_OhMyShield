using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Agent, IHealth
{
	[SerializeField] private AgentDataSO _agentData;

	private float _health;
	public float Health { get => _health; set => _health = value; }

	public override Agent Target { get; }

	private void Awake()
	{
		Init();
	}

	private void Init()
	{
		_health = _agentData.health;
	}
}
