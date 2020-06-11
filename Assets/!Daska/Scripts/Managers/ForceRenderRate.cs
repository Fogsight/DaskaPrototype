using System.Collections;
using System.Threading;
using UnityEngine;

//https://blogs.unity3d.com/2019/06/03/precise-framerates-in-unity/
public class ForceRenderRate : MonoBehaviour {
	public float Rate = 50.0f;
	private float currentFrameTime;

	private void Start() {
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 9999;
		currentFrameTime = Time.realtimeSinceStartup;
		StartCoroutine(WaitForNextFrame());
	}

	private IEnumerator WaitForNextFrame() {
		while (true) {
			yield return new WaitForEndOfFrame();
			currentFrameTime += 1.0f / Rate;
			var t = Time.realtimeSinceStartup;
			var sleepTime = currentFrameTime - t - 0.01f;
			if (sleepTime > 0)
				Thread.Sleep((int)(sleepTime * 1000));
			while (t < currentFrameTime)
				t = Time.realtimeSinceStartup;
		}
	}
}