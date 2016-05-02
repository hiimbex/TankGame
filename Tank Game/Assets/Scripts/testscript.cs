using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class testscript : MonoBehaviour {
	//Used for button callback.
	//Once clicked, will return player back to the tank level.
	public void ClickTest () {
		SceneManager.LoadScene ("Main Tank Level");	
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}
}
