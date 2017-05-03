using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOverlay : MonoBehaviour {

	private GameController gameController;
	// Use this for initialization
	void Start () {
		gameController = FindObjectOfType<GameController>();	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void FadeOutComplete(string sceneToLoad) {
		Debug.Log("fadeoutComplete!");
		gameController.LoadLevel(sceneToLoad);
	}
}
