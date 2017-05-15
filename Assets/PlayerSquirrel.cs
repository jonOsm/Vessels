using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSquirrel : MonoBehaviour {
	public bool isLinked;
	[HideInInspector]
	public bool isInLinkingRange;
	public Vector3 linkedOffset = new Vector3(0, 0.5f,0);
	void Start() {
		if (isLinked) Link();
	}

	void OnTriggerEnter(Collider col) {
		if (GameObject.FindGameObjectWithTag("Join Range")) {
			Debug.Log("in linking range");
			isInLinkingRange = true;
		}
	}

	void OnTriggerExit(Collider col) {
		if (GameObject.FindGameObjectWithTag("Join Range")) {
			isInLinkingRange = false;
		}
	}

	void LateUpdate() {
		if (isLinked) {
			transform.position = PlayerController.playerRobot.gameObject.transform.position + linkedOffset;
			transform.rotation = PlayerController.playerRobot.gameObject.transform.rotation;
		}
	}

	public void Link() {
		isLinked = true;	
		gameObject.GetComponent<BoxCollider>().isTrigger = true;
	}
	public void Unlink() {
		isLinked = false;
		gameObject.GetComponent<BoxCollider>().isTrigger = false;
	}

	public void ToggleLink() {
		if (isLinked) {
			Unlink();
		} else {
			Link();
		}
		if (GameController.currentState[GameState.SQUIRREL_ACTIVE])
			PlayerController.ToggleControl();
	}
}
