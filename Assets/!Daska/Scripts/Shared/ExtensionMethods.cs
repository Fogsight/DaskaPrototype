using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class ExtensionMethods {

	//Comparison
	/// <summary> Inclusive </summary>
	public static bool IsWithinRange<T>(this T value, T minimum, T maximum) where T : IComparable<T> {
		if (value.CompareTo(minimum) < 0) return false;
		if (value.CompareTo(maximum) > 0) return false;
		return true;
	}

	public static bool ApproximatelyEqual(this Vector2 value, Vector2 comparand, float magnitude = 0.01f) => Vector3.SqrMagnitude(value - comparand) < magnitude;

	public static bool ApproximatelyEqual(this Vector3 value, Vector3 comparand, float magnitude = 0.01f) => Vector3.SqrMagnitude(value - comparand) < magnitude;

	public static bool ApproximatelyEqual(this float value, float comparand, float magnitude = 0.01f) => Mathf.Abs(value - comparand) < magnitude;

	public static bool IsEqual(this Resolution value, Resolution comparand) => value.width == comparand.width && value.height == comparand.height;

	//Mirror
	/// <summary> Mirror a value within a given range </summary>
	public static float RangeMirror(this float value, float min, float max) => Mathf.Lerp(max, min, Mathf.InverseLerp(min, max, value));

	//Vector Additions
	public static Vector3 Add(this Vector3 value, Vector3 addend) => value += addend;

	public static Vector3 Add(this Vector3 value, float addend) => new Vector3(value.x + addend, value.y + addend, value.z + addend);

	public static Vector2 Add(this Vector2 value, Vector2 addend) => value += addend;

	public static Vector2 Add(this Vector2 value, float addend) => new Vector2(value.x + addend, value.y + addend);

	//2D conversion
	public static Vector2 ToVector2D(this Vector3 value) => new Vector2(value.x, value.z);

	public static Vector3 ToVector3D(this Vector2 value, float y = 0) => new Vector3(value.x, y, value.y);

	//Collections
	/// <summary>Removes and returns item from the list</summary>
	public static T RemoveAndGet<T>(this IList<T> list, int index) {
		lock (list) {
			T value = list[index];
			list.RemoveAt(index);
			return value;
		}
	}

	/// <summary>Adds and returns passed item</summary>
	public static T AddAndGet<T>(this IList<T> list, T item) {
		lock (list) {
			list.Add(item);
			return item;
		}
	}

	/// <summary>Adds and returns passed List</summary>
	public static List<T> AddRangeAndGet<T>(this List<T> list, List<T> item) {
		lock (list) {
			list.AddRange(item);
			return item;
		}
	}

	//Angles
	/// <summary>Split 360 to -180/180 angle rotation (for clamping)</summary>
	public static float TauToPi(this float angle) {
		angle %= 360;
		if (angle > 180) return angle - 360;
		return angle;
	}

	/// <summary>-180/180 to 360 angle rotation</summary>
	public static float PiToTau(this float angle) {
		if (angle >= 0) return angle;
		angle = -angle % 360;
		return 360 - angle;
	}

	/// <summary>Fisher–Yates shuffle</summary>
	public static void Shuffle<T>(this IList<T> list) {
		int count = list.Count;
		while (count > 1) {
			count--;
			int randomIndex = Random.Range(0, count + 1);
			T value = list[randomIndex];
			list[randomIndex] = list[count];
			list[count] = value;
		}
	}
}