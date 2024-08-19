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
	/// ���� �ʱ�ȭ
	/// </summary>
	/// <param name="attackTime">���ݿ� �ɸ��� �ð�</param>
	/// <param name="originPos">�ʱ� ��ġ ����</param>
	/// <param name="height">���� ����� ����(������ �߻�ü���� ���)</param>
	public virtual void SetValue(float attackTime, Vector2 originPos, float height = 0, float angle = 0)
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

		if (_isPool && !PoolManager.InstanceIsNull) // Ǯ���� ������Ʈ�̸� ��ȯ
			PoolManager.Instance.Push(this);
		else
			Destroy(gameObject);
	}

	public override void Blocked()
	{
		StopAttack();
	}

	public override void PoolInit()
	{
		// Ǯ���� ������Ʈ���� Ȯ��
		_isPool = true;
	}
}
