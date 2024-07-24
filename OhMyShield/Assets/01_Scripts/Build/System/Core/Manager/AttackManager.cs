using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoSingleton<AttackManager>
{
    private List<float> _attacks = new List<float>();

	private const float MinAttackDelay = 0.01f;

	public bool CanAttack(AttackBase attack)
	{
		_attacks.RemoveAll(num => num < Time.time);

		float attackTime = Time.time + attack.AttackTime;

		if (_attacks.Count > 0)
		{
			if (_attacks.Exists(num => Mathf.Abs(num - attackTime) <= MinAttackDelay))
			{
				return false;
			}
		}


		return true;
	}

	public void Attack(AttackBase attack, Agent target)
	{
		float attackTime = Time.time + attack.AttackTime;

		_attacks.Add(attackTime);
		attack.Attack(target);
	}
}
