using System.Collections;
using UnityEngine;

public class AIController : MonoBehaviour {
	[SerializeField] NoodleController noodleController = null;

	private void OnTriggerEnter2D(Collider2D collision) {
		if(collision.name == "Character")	noodleController.SwingNoodle();
	}
	private void OnTriggerExit2D(Collider2D collision) {
		if (collision.name == "Character") noodleController.SwingNoodle();
	}
}