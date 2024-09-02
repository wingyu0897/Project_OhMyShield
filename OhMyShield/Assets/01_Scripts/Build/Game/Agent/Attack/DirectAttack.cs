using System;

public class DirectAttack : AttackBase
{
	public override event Action<AttackBase> OnAttackEnd;

	public override void Attack(Agent target)
	{
		target.ModifyHealth(-Damage);
	}

	public override void StopAttack()
	{
		OnAttackEnd?.Invoke(this);
	}
}
