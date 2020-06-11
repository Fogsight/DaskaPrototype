using TMPro;
using UnityEngine;

public class DebugDisplay : MonoBehaviour {
	private TextMeshProUGUI debugText;
	public static DebugDisplay Instance { get; private set; }

	private void Awake() {
		debugText = GetComponent<TextMeshProUGUI>();
		if (Instance != null && Instance != this) Debug.LogError("There is another copy of this singleton", gameObject);
		else Instance = this;
	}

	public void DebugText(object callingObject, object text) {
		debugText.text = $"{callingObject}: {text}";
	}
}