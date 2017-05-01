using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	public Color playerLabelColor;
	public Color friendlyLabelColor;
	public Color neutralLabelColor;
	public Color enemyLabelColor;

	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Debug.Log("Should Esc on Windows");
			Application.Quit();
		}
	}

	public void LoadLevel(string levelName) {
		SceneManager.LoadScene("Prototype");
	}
	
	public void Quit() {
		Application.Quit();
	}
}
