﻿using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {

	public GameObject tankShell;
	public Transform tankShellSpawn;
	public float shootSpeed = 1000.0f;

	CharacterController cc;
	float rotateSpeed = 60f;
	float moveSpeed = 3f;
	float yVel = 0;
	float gravity = -0.5f;
	int score = 0;


	void Start () {
		cc = gameObject.GetComponent<CharacterController> ();
		GameManagerScript.player = this.gameObject; 

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

		if (Input.GetMouseButtonDown (0)) {
			GameObject tankShellClone = (GameObject)GameObject.Instantiate (tankShell, tankShellSpawn.position, tankShellSpawn.rotation );
			tankShellClone.GetComponent<Rigidbody> ().AddForce (tankShellSpawn.forward * shootSpeed); 
			//After 60 seconds, destroy arrow.
			Destroy (tankShellClone, 60f);
		}
	}

	void OnControllerColliderHit(ControllerColliderHit other) {
		//not currently in use but from old code in case it gets implemented
		if (other.gameObject.tag == "Road") {
			Debug.Log ("On the road");
			moveSpeed = moveSpeed + 3f;
		} else if (other.gameObject.tag == "Turret") {
			//moveSpeed = moveSpeed - 2f;
//		} else if (other.gameObject.CompareTag ("Pick Up")) {
//			//collecting objects so they disappear after collected and add them to the score
//			other.gameObject.SetActive (false);
//			moveSpeed = moveSpeed + 3f;
//			score++;
		} else if (other.gameObject.CompareTag ("Shell")) {
			//Debug.Log ("Player has been hit!");
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
		//sGUI.Label (new Rect(20, 20, 200, 200), "POWERUPS: " + score);
		GUI.Label (new Rect(20, 20, 400, 400), "Speed: " + moveSpeed + "0mph");
	}
}