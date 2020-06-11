using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoodleController : MonoBehaviour {
	public PlayerController PlayerController => playerController;
	[SerializeField] private PlayerController playerController = null;
	[SerializeField] private LayerMask noodleSnagMask = 0;
	[SerializeField] private LineRenderer noodleRenderer = null;
	[SerializeField] private NoodleSegment noodleSegmentPrefab = null;
	[SerializeField] private int numberOfAdditionalSegments = 10;
	[SerializeField] private float throwForce = 0.05f;
	[SerializeField] private float throwForceReduction = 0.1f;

	[SerializeField] private List<NoodleSegment> noodleSegments = null;

	//Swinging Hinge
	[SerializeField] private Rigidbody2D swingingHinge = null;
	private Coroutine animateSwing = null;
	private Vector2 swingPoint;
	private float Frequency {
		get => frequency; set {
			frequency = value;
			foreach (NoodleSegment noodleSegment in noodleSegments) {
				noodleSegment.Frequency = frequency;
			}
		}
	}
	[SerializeField] private float frequency = 10f;
	private void Start() {
		GenerateNoodle();
		StartCoroutine(RenderNoodle());
		StartCoroutine(NoodleSnagHandling());
	}

	internal void ThrowNoodle(Vector2 throwDirectionNormalized) {
		if (animateSwing is null) return;
		SwingNoodle(false);
		float modifiedThrowForce = throwForce;
		for (int i = noodleSegments.Count - 1; i >= 0; i--) {
			noodleSegments[i].RB2D.AddForce(throwDirectionNormalized * throwForce, ForceMode2D.Impulse);
			modifiedThrowForce *= throwForceReduction;
		}
	}

	private void OnDrawGizmos() {
		if (animateSwing == null) return;
		Gizmos.DrawWireSphere(swingPoint, .1f);
	}

	internal void SwingNoodleToggle() {
		if (animateSwing is null) SwingNoodle();
		else SwingNoodle(false);
	}
	internal void SwingNoodle(bool enabled = true) {
		if (enabled) {
			if (animateSwing is null) animateSwing = StartCoroutine(AnimateSwing());
		}
		else {
			if (animateSwing != null) {
				StopCoroutine(animateSwing);
				animateSwing = null;
			}
		}
	}

	private IEnumerator AnimateSwing() {
		NoodleSegment swingNoodleSegment = noodleSegments[noodleSegments.Count - 1 - 3];
		while (true) {
			yield return new WaitForFixedUpdate();
			swingPoint = swingingHinge.position + (Vector2)swingingHinge.transform.right;
			//print((Vector2)swingingHinge.transform.forward);
			swingNoodleSegment.RB2D.MovePosition(swingPoint);
		}
	}

	private IEnumerator NoodleSnagHandling() {
		//StringBuilder sb = new StringBuilder();
		while (true) {
			yield return new WaitForFixedUpdate();
			//sb.Clear();
			for (int i = 1; i < noodleSegments.Count; i++) {
				float rayLength = Vector3.Distance(noodleSegments[i].RB2D.transform.position, playerController.RB2D.transform.position + playerController.RBCenterOffset);
				Vector2 rayDirection = playerController.RB2D.transform.position + playerController.RBCenterOffset - noodleSegments[i].RB2D.transform.position;
				RaycastHit2D hit = Physics2D.Raycast(noodleSegments[i].RB2D.transform.position, rayDirection, rayLength, ~noodleSnagMask);

				//sb.AppendLine().Append(noodleSegments[i].name).Append(" ").Append(hit.collider == null).Append(" ").Append(hit.collider?.name);

				//Noodle segment collider control
				if (hit.collider == null) noodleSegments[i].SetColliderActive();
				else noodleSegments[i].SetColliderActive(false);
			}
			//DebugDisplay.Instance.DebugText(this, sb.ToString());
		}
	}

	private void GenerateNoodle() {
		Rigidbody2D connectedBody = noodleSegments[0].RB2D;
		for (int i = 0; i < numberOfAdditionalSegments; i++) {
			NoodleSegment noodleSegment = Instantiate(noodleSegmentPrefab, noodleSegments[0].RB2D.position, Quaternion.identity, playerController.TR).Init(frequency, connectedBody, i);
			connectedBody = noodleSegment.RB2D;
			noodleSegments.Add(noodleSegment);
		}
	}

	private IEnumerator RenderNoodle() {
		noodleRenderer.positionCount = noodleSegments.Count + 1;

		while (true) {
			yield return null;
			noodleRenderer.SetPosition(0, playerController.RB2D.transform.position + playerController.RBCenterOffset);
			for (int i = 0; i < noodleSegments.Count; i++) {
				noodleRenderer.SetPosition(i + 1, noodleSegments[i].RB2D.position);
			}
		}
	}
}