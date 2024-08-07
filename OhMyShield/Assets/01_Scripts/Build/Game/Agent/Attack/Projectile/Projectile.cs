using System;
using System.Collections;
using UnityEngine;

public class Projectile : AttackBase
{
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
	public virtual void SetValue(float attackTime, Vector2 originPos, float height)
	{
		_attackTime = attackTime;
		_originPos = originPos;
	}

	public override void Attack(Agent target)
	{
		if (_attackCoroutine != null)
			StopCoroutine(_attackCoroutine);

		_target = target;
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
		transform.position = Vector2.Lerp(_originPos, _target.transform.position, per);
	}

	public override void StopAttack()
	{
		if (_attackCoroutine != null)
		{
			StopCoroutine(_attackCoroutine);
			_attackCoroutine = null;
		}

		OnAttackEnd?.Invoke(this);

		if (_isPool) // 풀링된 오브젝트이면 반환
			PoolManager.Instance.Push(this);
		else
			Destroy(gameObject);
	}

	public override void PoolInit()
	{
		// 풀링된 오브젝트인지 확인
		_isPool = true;
	}
}
