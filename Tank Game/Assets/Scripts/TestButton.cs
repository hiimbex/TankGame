using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TestButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void onClick () {
		Debug.Log ("testing for scene change");
		SceneManager.LoadScene ("GameLostScene");	
	}
}
