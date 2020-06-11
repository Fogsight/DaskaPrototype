using System.Collections;
using UnityEngine;

/// <summary>
/// Canvas group activator with animation
/// </summary>
[RequireComponent(typeof(CanvasGroup))]
public class CanvasGroupActivator : MonoBehaviour {
	public CanvasGroup canvasGroup;
	private Coroutine fadeCR;
	private WaitForSecondsRealtime wfsr = new WaitForSecondsRealtime(.0166f);//60 fps animation
	internal bool Completed { get; private set; } = true;

	[ContextMenu("Toggle Display State")]
	private void ToggleDisplayState() {
		canvasGroup.blocksRaycasts = !canvasGroup.blocksRaycasts;
		if (canvasGroup.blocksRaycasts) canvasGroup.alpha = 1f;
		else canvasGroup.alpha = 0;
	}

	/// <summary> animation speed 0..1, 1 = instant transition </summary>
	internal void Activate(bool enable, float animationSpeed = 1f) {
		Completed = false;
		if (fadeCR != null) StopCoroutine(fadeCR);
		if (enable) fadeCR = StartCoroutine(FadeIn(animationSpeed));
		else fadeCR = StartCoroutine(FadeOut(animationSpeed));
	}

	private IEnumerator FadeOut(float animationSpeed = 1f) {
		canvasGroup.blocksRaycasts = false;
		while (canvasGroup.alpha > 0) {
			canvasGroup.alpha -= animationSpeed;
			yield return wfsr;
		}
		fadeCR = null;
		Completed = true;
	}

	private IEnumerator FadeIn(float animationSpeed = 1f) {
		canvasGroup.blocksRaycasts = true;
		while (canvasGroup.alpha < 1f) {
			canvasGroup.alpha += animationSpeed;
			yield return wfsr;
		}
		fadeCR = null;
		Completed = true;
	}

	private void OnValidate() => canvasGroup = GetComponent<CanvasGroup>();
}