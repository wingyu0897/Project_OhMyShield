using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoSingleton<AttackManager>
{
    private List<float> _attacks = new List<float>();

	private const float MinAttackDelay = 0.01f;

	public static bool CanAttack(float time)
	{
		_instance._attacks.RemoveAll(num => num < Time.time);

		float attackTime = Time.time + time;

		if (_instance._attacks.Count > 0)
		{
			if (_instance._attacks.Exists(num => Mathf.Abs(num - attackTime) <= MinAttackDelay))
			{
				return false;
			}
		}


		return true;
	}

	public void AddAttack(float attackTime)
	{
		float time = Time.time + attackTime;
		_attacks.Add(time);
	}
}
