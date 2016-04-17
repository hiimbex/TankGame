using UnityEngine;
using System.Collections;

public class EnemyTurretScript : MonoBehaviour {

	GameObject player;
	public GameObject projectile;
	public Transform projectileSpawn;
	public float shootSpeed = 1000.0f;

	public bool aimAtPlayer = false;

	// Use this for initialization
	void Start () {
		GameManagerScript.enemyTurretScriptList.Add (this); 
	}
	
	// Update is called once per frame
	void Update () {
		if (player == null) {
			player = GameManagerScript.player;
		}
		if (Vector3.Distance (transform.position, player.transform.position) < 50f) {
			aimAtPlayer = true;
		}
		if (aimAtPlayer) {
			Vector3 relativePos = player.transform.position - transform.localPosition;
			Debug.DrawRay (transform.position, relativePos*100f);
			transform.LookAt (player.transform.position);
			//transform.rotation = Quaternion.LookRotation (relativePos, Vector3.up);
//			Debug.Log ("Hello");
//			GameObject tankShellClone = (GameObject)GameObject.Instantiate (projectile, projectileSpawn.position, projectileSpawn.rotation );
//			tankShellClone.GetComponent<Rigidbody> ().AddForce (projectileSpawn.forward * shootSpeed); 
//			//After 60 seconds, destroy arrow.
//			Destroy (tankShellClone, 60f);
		}
	}
}
