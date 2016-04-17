using UnityEngine;
using System.Collections;

public class EnemyTurretScript : MonoBehaviour {

	public GameObject projectile;

	GameObject player;
	Transform projectileSpawn;
	Transform turret;
	float shootSpeed = 5000.0f;
	float timeToShoot;

	public bool aimAtPlayer = false;

	// Use this for initialization
	void Start () {
		GameManagerScript.enemyTurretScriptList.Add (this); 
		//Adding this to make sure it works. Will see later if removing the keyword
		//this will do anything.
		turret = this.transform.Find ("Turret Appearance/Turret");
		projectileSpawn = this.transform.Find ("Turret Appearance/Turret/ProjectileSpawn");
		timeToShoot = Time.deltaTime;
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
			turret.LookAt (player.transform);
			Debug.DrawRay (transform.position, relativePos*100f);

			if (timeToShoot > 1f) {
				GameObject projectileClone = (GameObject)GameObject.Instantiate (projectile, projectileSpawn.position, projectileSpawn.rotation);
				projectileClone.GetComponent<Rigidbody> ().AddForce (projectileSpawn.forward * shootSpeed);
				//After 60 seconds, destroy arrow.
				Destroy (projectileClone, 60f);
				timeToShoot = 0;
			}

			timeToShoot += Time.deltaTime;
		}
	}
}