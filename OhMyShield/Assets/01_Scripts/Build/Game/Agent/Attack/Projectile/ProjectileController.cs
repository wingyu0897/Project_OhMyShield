using System;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : AttackBase
{

	[Tooltip("Height of ParabolaProjectile")]
	[SerializeField] private float _height;
	[Tooltip("Angle for BezierProjectile")]
	[SerializeField] private float _angle;

	[Space]

	[SerializeField] private Projectile _projectilePrefab;

	private List<Projectile> _projectiles = new();

	public override event Action<AttackBase> OnAttackEnd;

	public override void Attack(Agent target)
	{
		// �߻�ü ����
		Projectile projectile = GameManager.Instance.poolManager.Pop(_projectilePrefab.name) as Projectile;
		if (projectile is null)
			projectile = Instantiate(_projectilePrefab);

		// �߻�ü ����
		projectile.transform.position = transform.position;
		projectile.SetValue(_attackTime, transform.position, _height, _angle);

		// �߻�ü ����
		projectile.Attack(target);
		projectile.OnAttackEnd += OnProjectileAttackEndHandler;
		_projectiles.Add(projectile);
	}

	private void OnProjectileAttackEndHandler(AttackBase attack)
	{
		Projectile projectile = attack as Projectile;
		projectile.OnAttackEnd -= OnProjectileAttackEndHandler;
		_projectiles.Remove(projectile);
		OnAttackEnd?.Invoke(this);
	}

	public override void StopAttack()
	{
		_projectiles.ForEach(proj => 
		{
			if (proj)
			{
				proj.OnAttackEnd -= OnProjectileAttackEndHandler;
				proj?.StopAttack();
			}
		});

		_projectiles.Clear();
	}

	public override void PoolInit() { }
}
