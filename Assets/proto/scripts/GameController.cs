using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
