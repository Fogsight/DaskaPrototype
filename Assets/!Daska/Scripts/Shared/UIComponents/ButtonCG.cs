using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Button with deactivation and fade animation
/// </summary>
[RequireComponent(typeof(CanvasGroup))]
public class ButtonCG : Button {
	internal CanvasGroup canvasGroup;
	private Coroutine fadeCR;
	private WaitForSecondsRealtime wfsr = new WaitForSecondsRealtime(.0166f);//60 fps animation

	protected override void Awake() => canvasGroup = GetComponent<CanvasGroup>();

	[ContextMenu("Toggle Display State")]
	private void ToggleDisplayState() {
		canvasGroup = GetComponent<CanvasGroup>();
		canvasGroup.blocksRaycasts = !canvasGroup.blocksRaycasts;
		if (canvasGroup.blocksRaycasts) canvasGroup.alpha = 1f;
		else canvasGroup.alpha = 0;
	}

	/// <summary> animation speed 0..1, 1 = instant transition </summary>
	internal void Activate(bool enable, float animationSpeed = 1f) {
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
	}

	private IEnumerator FadeIn(float animationSpeed = 1f) {
		canvasGroup.blocksRaycasts = true;
		while (canvasGroup.alpha < 1f) {
			canvasGroup.alpha += animationSpeed;
			yield return wfsr;
		}
		fadeCR = null;
	}
}