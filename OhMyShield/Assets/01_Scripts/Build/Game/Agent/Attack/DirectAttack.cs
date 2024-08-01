using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectAttack : AttackBase
{
	public override void Attack(Agent target)
	{
		target.ModifyHealth(-_damage);
	}

	public override void PoolInit()
	{
	}

	public override void StopAttack()
	{

	}
}
