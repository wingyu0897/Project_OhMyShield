using UnityEngine;

public class TwiceBezierProjectile : BezierProjectile
{
	protected Vector2 _blockedPos;
	protected bool _isSecond = false;

	public override void Blocked()
	{
		if (_isSecond)
		{
			StopAttack();
		}
		else
		{
			_isSecond = true;
			if (_attackCoroutine != null)
			{
				StopCoroutine(_attackCoroutine);
				_attackCoroutine = null;
			}

			_blockedPos = transform.position;
			_startPos = _blockedPos;
			_angle += Random.Range(10, 120) * Mathf.Sign(Random.Range(-1, 1));
			_angle = _angle > 180 ? -360f + _angle : _angle;

			SecondAttack();
		}
	}

	protected virtual void SecondAttack()
	{
		float dist = Vector2.Distance(_originPos, _target.transform.position) * 0.8f;
		_startAngle = _targetAngle - Mathf.PI * 2;
		_controlPointStart = _blockedPos + new Vector2(Mathf.Cos(_startAngle), Mathf.Sin(_startAngle)) * dist;
		_targetAngle = _angle * Mathf.Deg2Rad;
		_controlPointTarget = (Vector2)_target.transform.position + new Vector2(Mathf.Cos(_targetAngle), Mathf.Sin(_targetAngle)) * dist;

		_attackCoroutine = StartCoroutine(AttackCo());
	}

	public override bool ProjectileCanAttack()
	{
		return AttackManager.CanAttack(_attackTime) && AttackManager.CanAttack(_attackTime * 2);
	}

	public override void PoolInit()
	{
		base.PoolInit();

		_isSecond = false;
	}
}
