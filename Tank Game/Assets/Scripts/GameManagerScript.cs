using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


public class GameManagerScript : MonoBehaviour {

	public static List<EnemyTurretScript> enemyTurretScriptList = new List<EnemyTurretScript>();
	public static GameObject player;
	public static int enemyCount;
	public static int scoreCountEnemy;
	public Text turretsDestroyed;
	public Text playerHealth;

	// Use this for initialization
	void Start () {
		//make mouse disappear for better player experience
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	// Update is called once per frame
	void Update () {
		//displays how many turrets the player has destroyed
		turretsDestroyed.text = "Turrets Destroyed: " + scoreCountEnemy;

		//displays player health
		playerHealth.text = "Health: " + player.GetComponent<PlayerControls> ().health;

		//if player health is less than or equal to sero, player loses so redirect to game lost scene
		if (player.GetComponent<PlayerControls> ().health <= 0) {
			GameLost ();
		}
		//sets win condition
		if (enemyCount == 7){
			GameWon ();
		}
	}

	//Keeps track of how many turrets remain and removes turret if destroyed
	public static void ReduceEnemyCount (EnemyTurretScript turret) {
		enemyTurretScriptList.Remove (turret);
		scoreCountEnemy++;
		enemyCount -= 1;
		Debug.Log (enemyCount);
	}
	//redirects to lost game screen
	public static void GameLost () {
		SceneManager.LoadScene ("GameLostScene");	
		//unlocks mouse and allows for movement.
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}
	//redirects to victory screen
	void GameWon () {
		SceneManager.LoadScene ("GameWonScene");
		//unlocks mouse and allows for movement.
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}

}

