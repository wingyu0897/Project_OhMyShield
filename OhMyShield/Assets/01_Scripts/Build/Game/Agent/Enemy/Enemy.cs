using System;
using UnityEngine;

public class Enemy : Agent
{
	[SerializeField] private AgentDataSO _agentData;

	private float _health;
	public override float Health { get => _health; set => _health = value; }

	public override Agent Target { get; set; }
	public override event Action OnDie;

	public override void Die()
	{
		attacks.ForEach(attack => attack.StopAttack());
		OnDie?.Invoke();
		GameManager.Instance.poolManager.Push(this);
	}

	public override void Despawn()
	{
		attacks.ForEach(attack => attack.StopAttack());
		GameManager.Instance.poolManager.Push(this);
	}

	public override void PoolInit()
	{
		_health = _agentData.health;
	}

	private void OnDestroy()
	{
		attacks.ForEach(attack => attack.StopAttack());
	}
}
