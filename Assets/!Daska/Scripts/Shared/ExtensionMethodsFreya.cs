﻿public enum Ease { Linear, In, Out, InOut }

public static class EaseExtensions {

	public static float GetEased(this Ease ease, float t) {
		switch (ease) {
			case Ease.In: return t * t;
			case Ease.Out: return (2 - t) * t;
			case Ease.InOut: return -t * t * (2 * t - 3);
			default: return t;
		}
	}
}