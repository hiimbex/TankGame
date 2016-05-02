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
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		//make mouse disappear for better player experience
	}

	// Update is called once per frame
	void Update () {
		turretsDestroyed.text = "Turrets Destroyed: " + scoreCountEnemy;
		//displays how many turrets the player has destroyed

		playerHealth.text = "Health: " + player.GetComponent<PlayerControls> ().health;
		//displays player health

		if (player.GetComponent<PlayerControls> ().health <= 0) {
			GameLost ();
		}
		//if player health is less than or equal to sero, player loses so redirect to game lost scene

		if (enemyCount == 7){
			GameWon ();
		}
		//sets win condition
	}

	public static void ReduceEnemyCount (EnemyTurretScript turret) {
		enemyTurretScriptList.Remove (turret);
		scoreCountEnemy++;
		enemyCount -= 1;
		Debug.Log (enemyCount);
		//Keeps track of how many turrets remain and removes turret if destroyed
	}

	void GameLost () {
		SceneManager.LoadScene ("GameLostScene");	
	}
	//redirects to lost game screen

	void GameWon () {
		SceneManager.LoadScene ("GameWonScene");	
	}
	//redirects to victory screen

}

