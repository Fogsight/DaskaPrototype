using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StyleManagerUI : MonoBehaviour {
	[Header("Common Settings")]
	public bool wrapping = false;
	public TMP_FontAsset fontAsset;
	public Color tintColor = Color.white;

	//Default
	[Header("Default Font Settings")]
	public FontStyles defaultFontStyle = FontStyles.Normal;
	public float defaultFontSize = 36;
	[Header("Default Style Font")]
	public GameObject addDefault;
	public GameObject removeDefault;
	public List<TextMeshProUGUI> defaultStyleTextElements;

	//Other values

	private void OnValidate() {
		//Selection toggle area

		//Adding default entries
		if (addDefault != null) {
			AddEntries(addDefault);
			addDefault = null;
		}

		//Removing default entries
		if (removeDefault != null) {
			RemoveEntries(removeDefault);
			removeDefault = null;
		}
		ApplyAllUIChangesDefault();
	}

	[ContextMenu("Apply UI Changes For Default")]
	private void ApplyAllUIChangesDefault() {
		for (int i = defaultStyleTextElements.Count - 1; i >= 0; i--) {
			if (defaultStyleTextElements[i] == null) defaultStyleTextElements.RemoveAt(i);
			ApplyUIChanges(defaultStyleTextElements[i], defaultFontStyle, defaultFontSize);
		}
	}

	private void ApplyUIChanges(TextMeshProUGUI element, FontStyles fontStyle, float fontSize) {
		element.font = fontAsset;
		element.enableWordWrapping = wrapping;
		element.fontStyle = fontStyle;
		element.fontSize = fontSize;
		element.color = tintColor;
	}

	private void AddEntries(GameObject entries) {
		print($"<color=blue> Adding from {entries.name}</color>");
		TextMeshProUGUI[] incomingElements = entries.GetComponentsInChildren<TextMeshProUGUI>();
		for (int i = 0; i < incomingElements.Length; i++) {
			TextMeshProUGUI element = incomingElements[i];
			if (defaultStyleTextElements.Contains(element) is false) {
				defaultStyleTextElements.Add(element);
				ApplyUIChanges(element, defaultFontStyle, defaultFontSize);
			}
		}
	}

	private void RemoveEntries(GameObject entries) {
		print($"<color=blue> Removing from {entries.name}</color>");
		TextMeshProUGUI[] incomingElements = entries.GetComponentsInChildren<TextMeshProUGUI>();
		for (int i = 0; i < incomingElements.Length; i++) {
			defaultStyleTextElements.Remove(incomingElements[i]);
		}
	}
}