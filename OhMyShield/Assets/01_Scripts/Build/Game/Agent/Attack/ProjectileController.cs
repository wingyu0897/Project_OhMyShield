using UnityEngine;

public class ProjectileController : AttackBase
{
	[SerializeField] private Projectile _projectilePrefab;

	public override void Attack(Agent target)
	{
		Projectile projectile = GameManager.Instance.poolManager.Pop(_projectilePrefab.name) as Projectile;
		projectile.transform.position = transform.position;
		projectile.SetValue(_attackTime, transform.position);
		
		projectile.Attack(target);
	}

	public override void PoolInit()
	{

	}

	public override void StopAttack()
	{
		//Nothing
	}
}
