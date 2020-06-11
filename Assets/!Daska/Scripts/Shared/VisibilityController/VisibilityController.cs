using UnityEngine;

/// <summary>
/// Passes visibility events to another object
/// </summary>
public class VisibilityController : MonoBehaviour {
	[SerializeField] private MonoBehaviour visibilityControlScript = null;

	private void OnBecameVisible() => (visibilityControlScript as IVisibility).OnBecameVisible();

	private void OnBecameInvisible() => (visibilityControlScript as IVisibility).OnBecameInvisible();
}