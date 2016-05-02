using UnityEngine;
using System.Collections;

public class EnemyTurretScript : MonoBehaviour {

	public GameObject projectile;
	GameObject player;
	Transform projectileSpawn;
	Transform turret;
	float shootSpeed = 3000.0f;
	float timeToShoot;
	float randShootNum;
	public bool aimAtPlayer = false;

	// Use this for initialization
	void Start () {
		GameManagerScript.enemyTurretScriptList.Add (this); 
		GameManagerScript.enemyCount += 1;
		turret = this.transform.Find ("Turret Appearance/Turret");
		projectileSpawn = this.transform.Find ("Turret Appearance/Turret/ProjectileSpawn");
	}
	
	// Update is called once per frame
	void Update () {
		if (player == null) {
			player = GameManagerScript.player;
		}
		//makes sure player is still usabel as a oublic variable and calls it from other script if necessary

		if (Vector3.Distance (transform.position, player.transform.position) > 45f) {
			aimAtPlayer = false;
		}
		if (Vector3.Distance (transform.position, player.transform.position) < 45f) {
			aimAtPlayer = true;
		}
		//only starts firing if the player is within a certain distance
		//note we chose around 45 because it meant the turrets had some range 
		//but didn't fire ridiculously far or always like they did before we added this cap

			Vector3 relativePos = player.transform.position - transform.localPosition;
			turret.LookAt (player.transform);
		//makes turrets look at player in order to fire at them

			Debug.DrawRay (transform.position, relativePos * 100f);
		//raycasting used for debugging pruposes

		randShootNum = (Random.Range(2.5F, 8.0F));
		//added to randomize speed at which turrets fire

		if(aimAtPlayer){
			//if player is within turret range
		if (timeToShoot > randShootNum) {
			GameObject projectileClone = (GameObject)GameObject.Instantiate (projectile, projectileSpawn.position, projectileSpawn.rotation);
			projectileClone.GetComponent<Rigidbody> ().AddForce (projectileSpawn.forward * shootSpeed);
			//After 60 seconds, destroy arrow.
			Destroy (projectileClone, 60f);
			timeToShoot = 0;
		}
			timeToShoot += Time.deltaTime;
		}
		//Makes the turrets shoot at the player based on time and the initial random variable
	}


	void OnTriggerEnter (Collider other) {
		Debug.Log (this.gameObject.name);
		if (other.gameObject.CompareTag ("Shell")) {
			GameManagerScript.ReduceEnemyCount (this);
			Destroy (gameObject);
			Destroy (other.gameObject);
		}
	}
	//if a tank shell hits a turret destroy said turret and reduce the count of enemies
}