using UnityEngine;

public class NoodleSegment : MonoBehaviour {
	[SerializeField] private SpringJoint2D springJoint2D = null;
	[SerializeField] private Collider2D noodleCollider2D = null;
	private bool colliderEnabled = true;
	internal float Frequency {
		get => frequency;
		set {
			frequency = value;
			springJoint2D.frequency = frequency;
		}
	}
	[SerializeField] private float frequency = 10f;

	internal Rigidbody2D RB2D => rb2D;
	[SerializeField] private Rigidbody2D rb2D = null;

	internal NoodleSegment Init(float frequency, Rigidbody2D connectedRB2D, int id) {
		name = "NoodleSegment " + id;
		Frequency = frequency;
		springJoint2D.connectedBody = connectedRB2D;
		return this;
	}

	internal void SetColliderActive(bool enabled = true) {
		if (colliderEnabled == enabled) return;
		else colliderEnabled = noodleCollider2D.enabled = enabled;
	}
}