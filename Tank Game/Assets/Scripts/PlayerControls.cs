using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour {

	CharacterController cc;
	float rotateSpeed = 60f;
	float moveSpeed = 3f;
	float yVel = 0;
	float gravity = -0.5f;
	public Text ammoLeft;
	public Color originalAmmoTextColor;
	public GameObject tankShell;
	public Transform tankShellSpawn;
	public float shootSpeed = 1000.0f;
	public bool flashonoff = false;
	public int health = 10;
	public static int ammo = 10;

	void Start () {
		//gets character controller from tank and sends that to game manager script and gets original text color for ammo text
		cc = gameObject.GetComponent<CharacterController> ();
		GameManagerScript.player = this.gameObject; 
		originalAmmoTextColor = ammoLeft.color;
	}

	void Update () {

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
		//updates the ammo to display how much the player has left
		ammoLeft.text = "Ammo: " + ammo; 

		//makes ammo flash red to alert the user that they're out of ammo
		if (ammo == 0) {
			ammoLeft.color = Color.red;
			flashonoff = true;
			ammoLeft.color = new Color (ammoLeft.color.r, ammoLeft.color.g, ammoLeft.color.b, Mathf.PingPong (Time.time, 1));
		} else if (flashonoff && ammo > 0) {
			flashonoff = false;
			ammoLeft.color = originalAmmoTextColor;
		}
		//If the player has ammo and left clicks, then fire tank shell.
		if (Input.GetMouseButtonDown (0) && ammo > 0) {
			//Creates a new tank shell at the tank shell spawn location.
			GameObject tankShellClone = (GameObject)GameObject.Instantiate (tankShell, tankShellSpawn.position, tankShellSpawn.rotation );
			tankShellClone.GetComponent<Rigidbody> ().AddForce (tankShellSpawn.forward * shootSpeed); 
			//After 60 seconds, destroy tank Shell.
			Destroy (tankShellClone, 60f);
			ammo-=1;
		}
	}

	void OnTriggerEnter(Collider other) {
		//if ammo cache is picked up, set it's active to false and add 10 ammo 
		if (other.gameObject.tag == "Ammo") {
			other.gameObject.SetActive (false);
			ammo = ammo + 10;
			Debug.Log ("GOT AMMO!"); 
		} 
		//if player (read: tank) is hit with a shell subtract one health
		else if (other.gameObject.CompareTag ("Shell")) {
			health -= 1;
			Debug.Log (health);
		}
		//if player drives off the edge of the player field redirect to lost game screen
		if (other.name == "OutOfBoundsTrigger") {
			GameManagerScript.GameLost ();
		}
	}
}