using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadTrigger : MonoBehaviour {
	public string sceneName;	
	private GameController gameController;
	void Start() {
		gameController = FindObjectOfType<GameController>();
	}
	void OnTriggerEnter(Collider col) {
		if (col.gameObject.GetComponent<Interactor>()) {
			gameController.LoadLevel(sceneName);
		}
	}
}
