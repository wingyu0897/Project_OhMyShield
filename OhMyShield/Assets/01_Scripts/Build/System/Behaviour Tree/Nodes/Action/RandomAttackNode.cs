using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAttackNode : ActionNode
{
	protected override void OnStart()
	{

	}

	protected override void OnStop()
	{

	}

	protected override State OnUpdate()
	{
		var attacks = owner.attacks.FindAll(attack => attack.IsAbleToAttack());
		if (attacks.Count == 0)
			return State.Failure;

		AttackBase attack = attacks[Random.Range(0, attacks.Count)];
		attack.DoAttack(owner.Target);

		return State.Success;
	}
}
