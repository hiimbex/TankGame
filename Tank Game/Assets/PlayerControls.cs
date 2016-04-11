using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {

	CharacterController cc;
	float rotateSpeed = 60f;
	float jumpSpeed = 10f;
	float moveSpeed = 3f;
	float yVel = 0;
	float gravity = -0.5f;
	int score = 0;

	void Start () {
		cc = gameObject.GetComponent<CharacterController> ();
	}
		
	void Update () {

		//generic move code from mike
		float hAxis = Input.GetAxis ("Horizontal");
		float vAxis = Input.GetAxis ("Vertical");

		Vector3 amountToMove = Vector3.zero;
		Vector3 amountToRotate = Vector3.zero;

		amountToMove += transform.forward * vAxis * moveSpeed;
		amountToRotate.y += hAxis * rotateSpeed;
		amountToMove.y += yVel;

		//Gravity when moving up hills etc.
		if (!cc.isGrounded) {
			yVel += gravity;
		}
		amountToMove.y += yVel;

		//Apply changes to position and rotation
		amountToMove *= Time.deltaTime;
		amountToRotate *= Time.deltaTime;
		cc.Move (amountToMove);
		transform.Rotate (amountToRotate);

		//Update camera
		Camera.main.transform.position = transform.position - transform.forward * 5 + Vector3.up * 4;
		Camera.main.transform.LookAt (transform.position + Vector3.up * 2);
	}

	void OnTriggerEnter(Collider other) {
		//not currently in use but from old code in case it gets implemented
		if (other.gameObject.tag == "Road") {
			Debug.Log ("On the road");
			moveSpeed = moveSpeed + 3f;
		}
		if (other.gameObject.tag == "Obstacle") {
			moveSpeed = moveSpeed - 3f;
		}
		if (other.gameObject.CompareTag ("Pick Up")) {
			//collecting objects so they disappear after collected and add them to the score
			other.gameObject.SetActive (false);
			moveSpeed = moveSpeed + 3f;
			score++;
		} 
	}
	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Road") {
			Debug.Log ("On the road");
			moveSpeed = moveSpeed - 3f;
		}
	}
	void OnGUI() {
		//displays the score
		GUI.Label (new Rect(20, 20, 200, 200), "POWERUPS: " + score);
		GUI.Label (new Rect(40, 40, 400, 400), "SPEED: " + moveSpeed + "0mph");
	}
}