using Enums;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : AttackBase
{
	[SerializeField] private bool _overrideAttackProperties = false;

	[Header("Projectile")]
	[SerializeField] private PROJECTILE_TYPE _projectileType;
	[Tooltip("Height of ParabolaProjectile")]
	[SerializeField] private float _heightMin;
	[SerializeField] private float _heightMax;
	[SerializeField] private float _angle;
	[Tooltip("Angle for BezierProjectile")]
	[SerializeField] private bool _randomAngle;

	[Space]

	[SerializeField] private Projectile _projectilePrefab;

	private List<Projectile> _projectiles = new();

	public override event Action<AttackBase> OnAttackEnd;

	public override void Attack(Agent target)
	{
		// 발사체 생성
		Projectile projectile = PoolManager.Instance.Pop(_projectilePrefab.name) as Projectile;
		if (projectile is null)
			projectile = Instantiate(_projectilePrefab);

		// 발사체 설정
		float angle = _randomAngle ? UnityEngine.Random.Range(0, 360f) : _angle;
		float height = UnityEngine.Random.Range(_heightMin, _heightMax);
		projectile.projectileType = _projectileType;
		projectile.SetProjectileValue(transform.position, height, angle);
		if (_overrideAttackProperties)
		{
			projectile.Damage = Damage;
			projectile.AttackTime = AttackTime;
			projectile.CoolTime = CoolTime;
		}

		// 발사체 공격
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
			if (proj != null)
			{
				proj.OnAttackEnd -= OnProjectileAttackEndHandler;
				proj?.StopAttack();
			}
		});

		_projectiles.Clear();
	}

	public override bool CanAttack()
	{
		return base.CanAttack() && _projectilePrefab.ProjectileCanAttack();
	}
}
