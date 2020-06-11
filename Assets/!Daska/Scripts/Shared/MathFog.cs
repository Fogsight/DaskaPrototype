using UnityEngine;

public static class MathFog {
	public const float TAU = 6.28318530718f;

	/// <summary> Vector3 Inverse Linear Interpolation </summary>
	public static float InverseVectorLerp(Vector3 a, Vector3 b, Vector3 value) {
		Vector3 ab = b - a;
		Vector3 av = value - a;
		return Vector3.Dot(av, ab) / Vector3.Dot(ab, ab);
	}

	/// <summary> Vector2 Inverse Linear Interpolation </summary>
	public static float InverseVectorLerp(Vector2 a, Vector2 b, Vector2 value) {
		Vector2 ab = b - a;
		Vector2 av = value - a;
		return Vector2.Dot(av, ab) / Vector2.Dot(ab, ab);
	}

	/// <summary> Returns closest angle -180/180 to the target </summary>
	public static float ClosestRotationToTarget(Vector2 targetPos, Vector2 pos, Vector2 facingDirection) {
		var endRotation = targetPos - pos;
		return Vector2.SignedAngle(endRotation, facingDirection);
	}
}