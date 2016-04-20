using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManagerScript : MonoBehaviour {

	public static List<EnemyTurretScript> enemyTurretScriptList = new List<EnemyTurretScript>();
	public static GameObject player;
	//This works for now but it would be better to just get the length of the enemyTurretScriptList.
	public static int enemyCount;
	// Use this for initialization
	void Start () {
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static void ReduceEnemyCount (EnemyTurretScript turret) {
		enemyTurretScriptList.Remove (turret);
		enemyCount -= 1;
		Debug.Log (enemyCount);
	}
}
