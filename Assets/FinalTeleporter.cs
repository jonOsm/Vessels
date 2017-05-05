using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalTeleporter : MonoBehaviour {

	public float timeUntilSceneEnd;
	private GameController gameController;
	// Use this for initialization
	void Start () {
		gameController = FindObjectOfType<GameController>();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter() {
		PlayerPrefs.SetInt("prototypeComplete", 1);
		gameController.EndPrototype();
		Invoke("EndCurrentScene", timeUntilSceneEnd);
	}

	void EndCurrentScene() {
		gameController.FadeOutOfScene();
	}
}
