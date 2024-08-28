using System;
using System.Collections;
using UnityEngine;

public class Projectile : AttackBase
{
	[SerializeField] protected bool _angleToDirection = false;

	protected Vector2 _startPos;
	protected Vector2 _endPos;

	protected Agent _target;
	protected Vector2 _originPos;
	protected float _time;

	protected bool _isPool = false;

	protected Coroutine _attackCoroutine;

	public override event Action<AttackBase> OnAttackEnd;

	/// <summary>
	/// 변수 초기화
	/// </summary>
	/// <param name="attackTime">공격에 걸리는 시간</param>
	/// <param name="originPos">초기 위치 정보</param>
	/// <param name="height">위로 띄워질 높이(포물선 발사체에서 사용)</param>
	public virtual void SetValue(Vector2 originPos, float height = 0, float angle = 0)
	{
		_originPos = originPos;
	}

	public override void Attack(Agent target)
	{
		if (_attackCoroutine != null)
			StopCoroutine(_attackCoroutine);

		_target = target;
		_startPos = _originPos;
		_endPos = _target.transform.position;

		_attackCoroutine = StartCoroutine(AttackCo());
	}

	protected virtual IEnumerator AttackCo()
	{
		_time = 0;
		float per;
		while (_time < _attackTime)
		{
			per = _time / _attackTime;
			SetPosition(per);

			_time += Time.deltaTime;
			yield return null;
		}

		StopAttack();
		yield return null;
	}

	protected virtual void SetPosition(float per)
	{
		transform.position = Vector2.Lerp(_startPos, _endPos, per);
		if (_angleToDirection)
			AngleToDirection(_endPos - _startPos);
	}

	protected void AngleToDirection(Vector2 direction)
	{
		direction.Normalize();
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
		transform.rotation = Quaternion.Euler(0, 0, angle);
	}

	public override void StopAttack()
	{
		if (_attackCoroutine != null)
		{
			StopCoroutine(_attackCoroutine);
			_attackCoroutine = null;
		}

		OnAttackEnd?.Invoke(this);

		if (_isPool && !PoolManager.InstanceIsNull) // 풀링된 오브젝트이면 반환
			PoolManager.Instance.Push(this);
		else
			Destroy(gameObject);
	}

	public virtual bool ProjectileCanAttack()
	{
		return AttackManager.CanAttack(_attackTime);
	}

	public override void PoolInit()
	{
		// 풀링된 오브젝트인지 확인
		_isPool = true;
	}
}
