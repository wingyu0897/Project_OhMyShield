using UnityEngine;

public static class ProjectilePositionCalculator
{
	public static float AngleToDirection(Vector2 direction)
	{
		direction.Normalize();
		return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
	}

	public static Vector2 Straight(Vector2 start, Vector2 end, float percentage, out float angle)
	{
		angle = AngleToDirection(end - start);
		return Vector2.Lerp(start, end, percentage);
	}

	public static Vector2 Parabola(Vector2 start, Vector2 end, float percentage, float height, out float angle)
	{
		angle = 0;
		float x = Mathf.Lerp(start.x, end.x, percentage);
		float y = start.y + Mathf.Sin(percentage * Mathf.PI) * height;
		return new Vector3(x, y);
	}

	public static Vector2 Bezier(Vector2 start, Vector2 end, float percentage, Vector2 controlPoint1, Vector2 controlPoint2, out float angle)
	{
		Vector2 linearFrom = Vector2.Lerp(start, controlPoint1, percentage);
		Vector2 linearMid = Vector2.Lerp(controlPoint1, controlPoint2, percentage);
		Vector2 linearTo = Vector2.Lerp(controlPoint2, end, percentage);

		Vector2 linearFromMid = Vector2.Lerp(linearFrom, linearMid, percentage);
		Vector2 linearMidTo = Vector2.Lerp(linearMid, linearTo, percentage);

		Vector2 point = Vector2.Lerp(linearFromMid, linearMidTo, percentage);

		angle = AngleToDirection(linearMidTo - linearFromMid);

		return point;
	}
}
