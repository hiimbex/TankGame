using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour {

	public static List<EnemyTurretScript> enemyTurretScriptList = new List<EnemyTurretScript>();
	public static GameObject player;
	//This works for now but it would be better to just get the length of the enemyTurretScriptList.
	public static int enemyCount;
	public static int scoreCountEnemy;
	public Text turretsDestroyed;
	// Use this for initialization

	void Start () {
<<<<<<< HEAD
		//turretsDestroyed = gameObject.GetComponent<Text>();
=======
>>>>>>> 38dcc8bbce42f43b2c57d21d89fedf5761daefd2
		//Cursor.visible = false;
		//Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {
		//scoreCountEnemy = scoreCountEnemy.ToString ();
		turretsDestroyed.text = "Turrets Destroyed: " + scoreCountEnemy;
	}

	public static void ReduceEnemyCount (EnemyTurretScript turret) {
		enemyTurretScriptList.Remove (turret);
		scoreCountEnemy++;
		enemyCount -= 1;
		Debug.Log (enemyCount);
	}

	void OnGUI() {
		GUI.Label (new Rect(20, 40, 400, 400), "Turrets Destroyed: " + scoreCountEnemy);
	}
}
