using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalTeleporter : MonoBehaviour {

	private GameController gameController;
	// Use this for initialization
	void Start () {
		gameController = FindObjectOfType<GameController>();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter() {
		gameController.EndPrototype();
	}
}
