using UnityEngine;
using Random = UnityEngine.Random;

namespace GeometryUtils {

	public static class TriangleUtils {

		/// <summary> Returns local space points relative to the triangle. </summary>
		public static Vector2 GetRandomPosInTriangle(Vector2 v0, Vector2 v1, Vector2 v2) {
			Vector2 tv1 = v1 - v0;
			Vector2 tv2 = v2 - v0;
			//Picks random vector length
			float a1 = Random.Range(0, 1f);
			float a2 = Random.Range(0, 1f);
			//Picks random point on the triangle or its mirror for an even distribution
			Vector2 testPoint = a1 * tv1 + a2 * tv2;
			//Flips results from the mirrored triangle to the original
			return (IsPointInsideTriangle(testPoint, Vector3.zero, tv1, tv2)) ? testPoint : tv1 + tv2 - testPoint;
		}

		private static float Sign(Vector2 v0, Vector2 v1, Vector2 v2) => (v0.x - v2.x) * (v1.y - v2.y) - (v1.x - v2.x) * (v0.y - v2.y);

		public static bool IsPointInsideTriangle(Vector2 testPoint, Vector2 v0, Vector2 v1, Vector2 v2) {
			bool b1 = Sign(testPoint, v0, v1) < 0;
			bool b2 = Sign(testPoint, v1, v2) < 0;
			bool b3 = Sign(testPoint, v2, v0) < 0;
			return ((b1 == b2) && (b2 == b3));
		}
	}

	public static class QuadrilateralUtils {

		public static bool IsPointInsideQuadrilateral(Vector2 testPoint, Vector2 v0, Vector2 v1, Vector2 v2, Vector2 v3) =>
			TriangleUtils.IsPointInsideTriangle(testPoint, v0, v1, v2) || TriangleUtils.IsPointInsideTriangle(testPoint, v0, v2, v3);

		public static Vector2 GetRandomPosInQuadrilateral(Vector2 v0, Vector2 v1, Vector2 v2, Vector2 v3) {
			if (Random.Range(0, 1f) < .5f) return TriangleUtils.GetRandomPosInTriangle(v0, v1, v2);
			else return TriangleUtils.GetRandomPosInTriangle(v0, v2, v3);
		}
	}
}