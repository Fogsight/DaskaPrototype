using UnityEngine;

public class PlayerController : MonoBehaviour {
	public Transform TR { get; private set; } = null;
	internal Rigidbody2D RB2D => rb2D;
	[SerializeField] private Rigidbody2D rb2D = null;
	public SpriteRenderer characterSR = null;
	public float gravityCorrection = 100f;
	internal float HorizontalForce { get; set; } = 0;
	public float forceModifyer = 20f;
	public float stoppingForce = 1.05f;

	internal Vector3 RBCenterOffset => rbCenterOffset;
	[SerializeField] private Vector3 rbCenterOffset = new Vector3(0, 2.2f);
	//Noodle
	public NoodleController NoodleController => noodleController;
	[SerializeField] private NoodleController noodleController = null;

	//Ground test
	[SerializeField] private float groundCastRadius = .65f;
	[SerializeField] private Vector3 groundCastOffset = new Vector3(0, .5f);
	[SerializeField] internal bool IsGrounded { get; private set; } = false;

	[SerializeField] private LayerMask groundCastMask = 0; //Exclusion mask, add character layer to test only against everything else.

	private void Awake() {
		TR = transform;
	}

	private void GroundTest() {
		Collider2D contact = Physics2D.OverlapCircle(TR.position + groundCastOffset, groundCastRadius, ~groundCastMask);
		if (contact is null) IsGrounded = false;
		else IsGrounded = true;
	}

	private void FacingDirection() {
		if (HorizontalForce < 0) {
			characterSR.flipX = true;
		}
		else if (HorizontalForce > 0) characterSR.flipX = false;
	}

	private void FixedUpdate() {
		GroundTest();
		FacingDirection();
		//Accelerated stop
		if (HorizontalForce == 0 && RB2D.velocity.x != 0) {
			Vector2 velocity = RB2D.velocity;
			velocity.x /= stoppingForce;
			RB2D.velocity = velocity;
		}

		//Base mobility
		RB2D.AddForce(new Vector2(HorizontalForce * forceModifyer, -gravityCorrection));
	}

	private void OnDrawGizmos() {
		if (TR == null) return;
		Gizmos.DrawWireSphere(TR.position + groundCastOffset, groundCastRadius);
	}
}