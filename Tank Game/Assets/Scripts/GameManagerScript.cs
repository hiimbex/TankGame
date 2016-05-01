using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


public class GameManagerScript : MonoBehaviour {

	public static List<EnemyTurretScript> enemyTurretScriptList = new List<EnemyTurretScript>();
	public static GameObject player;
	//This works for now but it would be better to just get the length of the enemyTurretScriptList.
	public static int enemyCount;
	public static int scoreCountEnemy;
	public Text turretsDestroyed;
	public Text playerHealth;

	// Use this for initialization

	void Start () {
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	// Update is called once per frame
	void Update () {
		turretsDestroyed.text = "Turrets Destroyed: " + scoreCountEnemy;
		playerHealth.text = "Health: " + player.GetComponent<PlayerControls> ().health;
		if (player.GetComponent<PlayerControls> ().health <= 0) {
			GameLost ();
		}
		if (enemyCount == 5){
			GameWon ();
		}
	}

	public static void ReduceEnemyCount (EnemyTurretScript turret) {
		enemyTurretScriptList.Remove (turret);
		scoreCountEnemy++;
		enemyCount -= 1;
		Debug.Log (enemyCount);
	
	}

	void GameLost () {
		SceneManager.LoadScene ("GameLostScene");	
	}

	void GameWon () {
		SceneManager.LoadScene ("GameWonScene");	
	}

}

