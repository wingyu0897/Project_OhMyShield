using UnityEngine;

/// <summary>
/// 포물선 발사체
/// </summary>
public class ParabolaProjectile : Projectile
{
	[SerializeField] protected float _height;

	public override void SetValue(float attackTime, Vector2 originPos, float height)
	{
		base.SetValue(attackTime, originPos, height);
		_height = height;
	}

	protected override void SetPosition(float per)
	{
		float x = Mathf.Lerp(_originPos.x, _target.transform.position.x, per);
		float y = _originPos.y + Mathf.Sin(per * Mathf.PI) * _height;
		transform.position = new Vector3(x, y);
	}
}
