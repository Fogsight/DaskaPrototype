using UnityEngine;
using UnityEngine.SceneManagement;

//Todo: Make Input manager spawnable for each player
public class InputManager : MonoBehaviour {
	[SerializeField] private PlayerController playerController = null;
	public float jumpForce = 20f;
	private Vector2 throwTargetPoint;
	private Vector2 throwDirection;

	private void Update() {
		TargetingUpdate();
		playerController.HorizontalForce = Input.GetAxis("Horizontal");
		if (Input.GetKeyDown(KeyCode.Space) && playerController.IsGrounded) playerController.RB2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
		if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(0);
		if (Input.GetKeyDown(KeyCode.E)) { playerController.NoodleController.SwingNoodleToggle(); };
	}

	private void TargetingUpdate() {
		if (Input.GetKey(KeyCode.Mouse0)) {
			throwTargetPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition); //Flattens to 2d
			Debug.DrawLine(playerController.TR.position + playerController.RBCenterOffset, throwTargetPoint, Color.blue);
			throwDirection = throwTargetPoint - ((Vector2)playerController.TR.position + (Vector2)playerController.RBCenterOffset);
		}
		if (Input.GetKeyUp(KeyCode.Mouse0)) {
			playerController.NoodleController.ThrowNoodle(throwDirection.normalized);
		}
	}
}