using Enums;
using System;
using System.Collections;
using UnityEngine;

public class Projectile : AttackBase
{
	public PROJECTILE_TYPE projectileType = PROJECTILE_TYPE.Straight;

	[Tooltip("The projectile rotates in the direction of travel")]
	[SerializeField] protected bool _angleToDirection = false;

	protected bool _isPool = false; // ���� ������Ʈ�� Ǯ���� �����Ǿ����� �����Ͽ� ���ŵ� �� �ı� �Ǵ� Ǯ�� �ǵ��� ����.

	protected Vector2 _originPos;
	protected Vector2 _startPos;
	protected Vector2 _endPos;

	protected float _time;

	protected Agent _target;
	protected Coroutine _attackCoroutine;

	public override event Action<AttackBase> OnAttackEnd;

	[Header("Projectile Values")]
	protected float _height;
	protected float _angle;
	protected float _startAngle;
	protected float _targetAngle;
	protected Vector2 _controlPoint1;
	protected Vector2 _controlPoint2;

	/// <summary>
	/// �߻�ü ���� ���� �ʱ�ȭ
	/// </summary>
	/// <param name="originPos">�ʱ� ��ġ ����</param>
	/// <param name="height">���� ����� ����(Parabola)</param>
	/// <param name="angle">���� ����(Bezier)</param>
	public virtual void SetProjectileValue(Vector2 originPos, float height = 0, float angle = 0)
	{
		_originPos = originPos;
		_height = height;
		_angle = angle > 180 ? -360f + angle : angle;
	}

	public override void Attack(Agent target)
	{
		if (_attackCoroutine != null)
			StopCoroutine(_attackCoroutine);

		BeforeAttack(target);
		_attackCoroutine = StartCoroutine(AttackCo());
	}

	private void BeforeAttack(Agent target)
	{
		_target = target;
		_startPos = _originPos;
		_endPos = _target.transform.position;

		if (projectileType == PROJECTILE_TYPE.Bezier)
		{
			float mulByAngle = Mathf.Abs(_angle) > 90f ? (Mathf.Abs(_angle) - 90f) / 180f + 0.5f : 0.5f;
			float dist = Vector2.Distance(_originPos, target.transform.position) * mulByAngle;
			_startAngle = (180f - _angle * 0.5f) * Mathf.Deg2Rad;
			_controlPoint1 = _originPos + new Vector2(Mathf.Cos(_startAngle), Mathf.Sin(_startAngle)) * dist;
			_targetAngle = _angle * Mathf.Deg2Rad;
			_controlPoint2 = (Vector2)target.transform.position + new Vector2(Mathf.Cos(_targetAngle), Mathf.Sin(_targetAngle)) * dist;
		}
	}

	protected virtual IEnumerator AttackCo()
	{
		_time = 0;
		float per;
		while (_time < AttackTime)
		{
			per = _time / AttackTime;
			SetPosition(per);

			_time += Time.deltaTime;
			yield return null;
		}

		StopAttack();
		yield return null;
	}

	protected virtual void SetPosition(float per)
	{
		float angle;

		Vector2 pos = projectileType switch
		{
			PROJECTILE_TYPE.Straight => ProjectilePositionCalculator.Straight(_startPos, _endPos, per, out angle),
			PROJECTILE_TYPE.Parabola => ProjectilePositionCalculator.Parabola(_startPos, _endPos, per, _height, out angle),
			PROJECTILE_TYPE.Bezier => ProjectilePositionCalculator.Bezier(_startPos, _endPos, per, _controlPoint1, _controlPoint2, out angle),
			_ => ProjectilePositionCalculator.Straight(_startPos, _endPos, per, out angle),
		};

		transform.position = pos;
		if (_angleToDirection)
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

		if (_isPool && !PoolManager.InstanceIsNull) // Ǯ���� ������Ʈ�̸� ��ȯ
			PoolManager.Instance.Push(this);
		else
			Destroy(gameObject);
	}

	public virtual bool ProjectileCanAttack()
	{
		return AttackManager.CanAttack(AttackTime);
	}

	public override void PoolInit()
	{
		if (_attackCoroutine != null)
		{
			print("�̰��� ���� �߿� �� �Ȱſ�");
		}

		// Ǯ���� ������Ʈ���� Ȯ��
		_isPool = true;
	}
}
