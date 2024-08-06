using System;
using UnityEngine;

public class Player : Agent
{
	[SerializeField] private AgentDataSO _agentData;

	private float _health;
	public override float Health { get => _health; set => _health = value; }
	private Agent _target;

	public override Agent Target { get => _target; set => _target = value; }

	public override event Action OnDie;

	public void Attack()
	{
		if (attacks.Count == 0 || _target == null) return;

		attacks[0].Attack(_target);
	}

	public override void Dead()
	{
		OnDie?.Invoke();
		EditorLog.Log("Player Dead");
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent(out AttackBase attack))
		{
			ModifyHealth(-attack.Damage);
		}
	}

	public override void PoolInit()
	{
		_health = _agentData.health;
	}
}
