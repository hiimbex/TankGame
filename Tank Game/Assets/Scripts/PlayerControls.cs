using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour {

	public GameObject tankShell;
	public Transform tankShellSpawn;
	public float shootSpeed = 1000.0f;

	CharacterController cc;
	float rotateSpeed = 60f;
	float moveSpeed = 3f;
	float yVel = 0;
	float gravity = -0.5f;
	public static int ammo = 10;
	public Text ammoLeft;
	public int health = 10;
	public bool flashonoff = false;
	public Color originalAmmoTextColor;


	void Start () {
		cc = gameObject.GetComponent<CharacterController> ();
		GameManagerScript.player = this.gameObject; 
		originalAmmoTextColor = ammoLeft.color;
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

		ammoLeft.text = "Ammo: " + ammo; 
		//updates the ammo through the ui

		if (ammo == 0) {
			ammoLeft.color = Color.red;
			flashonoff = true;
			ammoLeft.color = new Color (ammoLeft.color.r, ammoLeft.color.g, ammoLeft.color.b, Mathf.PingPong (Time.time, 1));
		} else if (flashonoff && ammo > 0) {
			flashonoff = false;
			ammoLeft.color = originalAmmoTextColor;
		}
		//makes ammo flash red to alert the user that they're out of ammo

		if (Input.GetMouseButtonDown (0) && ammo > 0) {
			GameObject tankShellClone = (GameObject)GameObject.Instantiate (tankShell, tankShellSpawn.position, tankShellSpawn.rotation );
			tankShellClone.GetComponent<Rigidbody> ().AddForce (tankShellSpawn.forward * shootSpeed); 
			//After 60 seconds, destroy tank Shell.
			Destroy (tankShellClone, 60f);
			ammo-=1;
		}
		//uses mouse down to fire ammo from tank provided the player is not out of ammo
	}

	void OnTriggerEnter(Collider other) {

		if (other.gameObject.tag == "Ammo") {
			other.gameObject.SetActive (false);
			ammo = ammo + 10;
			Debug.Log ("GOT AMMO!"); 
		} 
		//if ammo cache is picked up, set it's active to false and add 10 ammo 
		else if (other.gameObject.CompareTag ("Shell")) {
			health -= 1;
			Debug.Log (health);
		}
		//if player (read: tank) is hit with a shell subtract one health
		if (other.name == "OutOfBoundsTrigger") {
			GameLost ();
		}
		//if player drives off the edge of the player field redirect to lost game screen
	}

	void GameLost () {
		SceneManager.LoadScene ("GameLostScene");	
	}
	//function to redirect to lost game screen
}