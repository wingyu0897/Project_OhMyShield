using System.Collections;
using UnityEngine;

public class Projectile : AttackBase
{
	private Vector2 _originPos;
	private float _time;

	private Coroutine _attackCoroutine;

	public void SetValue(float attackTime, Vector2 originPos)
	{
		_attackTime = attackTime;
		_originPos = originPos;
	}

	public override void Attack(Agent target)
	{
		if (_attackCoroutine != null)
			StopCoroutine(_attackCoroutine);

		_attackCoroutine = StartCoroutine(AttackCo(target));
	}

	private IEnumerator AttackCo(Agent target)
	{
		_time = 0;
		float per = 0;
		while (_time < _attackTime)
		{
			per = _time / _attackTime;
			transform.position = Vector2.Lerp(_originPos, target.transform.position, per);

			_time += Time.deltaTime;

			yield return null;
		}

		StopAttack();

		yield return null;
	}

	public override void StopAttack()
	{
		if (_attackCoroutine != null)
		{
			StopCoroutine(_attackCoroutine);
		}

		GameManager.Instance.poolManager.Push(this);
	}

	public override void PoolInit()
	{
		_attackTime = 1f;
	}
}
