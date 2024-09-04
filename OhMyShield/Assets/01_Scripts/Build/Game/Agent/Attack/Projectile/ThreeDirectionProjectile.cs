using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ThreeDirectionProjectile : AttackBase
{
	[SerializeField] private bool _overrideAttackTime = true;
	[Space]
	[SerializeField] private float _animationTime = 1f;
    [SerializeField] private float _throwingDistance = 2f;
	[SerializeField] private Projectile _projectilePrefab;

	private List<Projectile> _projectileOnAnimation = new();

	public override void Attack(Agent target)
	{
		Projectile projectile = PoolManager.Instance.Pop(_projectilePrefab.name) as Projectile;
		if (projectile == null) return;
		projectile.projectileType = Enums.PROJECTILE_TYPE.Straight;

		int rand = Random.Range(-1, 2);
		Vector2 pos = new Vector3(rand == 0 ? 1 : 0, rand) * _throwingDistance + transform.position;
		projectile.transform.position = transform.position;
		projectile.transform.rotation = Quaternion.Euler(0, 0, 90);

		if (_overrideAttackTime)
			projectile.AttackTime = AttackTime;
		projectile.SetProjectileValue(pos);

		projectile.transform.DOKill();

		_projectileOnAnimation.Add(projectile);
		projectile.transform.DOMove(pos, _animationTime).OnComplete(() => {
			_projectileOnAnimation.Remove(projectile);
			projectile.Attack(target);
		});
	}

	public override void StopAttack()
	{
		foreach (Projectile projectile in _projectileOnAnimation)
		{
			projectile.transform.DOKill();
			projectile.StopAttack();
		}
		_projectileOnAnimation.Clear();
	}
}
