using UnityEngine;

public class BezierProjectile : Projectile
{
	[SerializeField] private float _angle;

	private float _startAngle;
	private float _targetAngle;
	private Vector2 _controlPointStart;
	private Vector2 _controlPointTarget;

	public override void SetValue(float attackTime, Vector2 originPos, float height = 0, float angle = 0)
	{
		base.SetValue(attackTime, originPos, height, angle);
		_angle = angle > 180 ? -360f + angle : angle;
	}

	public override void Attack(Agent target)
	{
		float mulByAngle = Mathf.Abs(_angle) > 90f ? (Mathf.Abs(_angle) - 90f) / 180f + 0.5f : 0.5f;
		float dist = Vector2.Distance(_originPos, target.transform.position) * mulByAngle;
		_startAngle = (180f - _angle * 0.5f) * Mathf.Deg2Rad;
		_controlPointStart = _originPos + new Vector2(Mathf.Cos(_startAngle), Mathf.Sin(_startAngle)) * dist;
		_targetAngle = _angle * Mathf.Deg2Rad;
		_controlPointTarget = (Vector2)target.transform.position + new Vector2(Mathf.Cos(_targetAngle), Mathf.Sin(_targetAngle)) * dist;

		base.Attack(target);
	}

	protected override void SetPosition(float per)
	{
		//Debug.DrawLine(_originPos, _controlPointStart, Color.blue);
		//Debug.DrawLine(_controlPointStart, _controlPointTarget, Color.blue);
		//Debug.DrawLine(_controlPointTarget, _target.transform.position, Color.blue);
		Vector2 linearFrom = Vector2.Lerp(_originPos, _controlPointStart, per);
		Vector2 linearMid = Vector2.Lerp(_controlPointStart, _controlPointTarget, per);
		Vector2 linearTo = Vector2.Lerp(_controlPointTarget, _target.transform.position, per);

		//Debug.DrawLine(linearFrom, linearMid, Color.green);
		//Debug.DrawLine(linearMid, linearTo, Color.green);
		Vector2 linearFromMid = Vector2.Lerp(linearFrom, linearMid, per);
		Vector2 linearMidTo = Vector2.Lerp(linearMid, linearTo, per);

		//Debug.DrawLine(linearFromMid, linearMidTo, Color.red);
		Vector2 point = Vector2.Lerp(linearFromMid, linearMidTo, per);

		transform.position = point;
	}
}
