using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class testscript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log ("testing for scene change start");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ClickTest () {
		Debug.Log ("testing for scene change function");
		SceneManager.LoadScene ("Main Tank Level");	
	
	}
}
