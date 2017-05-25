using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSquirrel : MonoBehaviour {
	public bool isLinked;
	[HideInInspector]
	public bool isInLinkingRange;
	public Vector3 linkedOffset = new Vector3(0, 0.5f,0);

	private float startMass;

	void Start() {
		if (isLinked) {
			isInLinkingRange = true;
			Link();
		}
		startMass = gameObject.GetComponent<Rigidbody>().mass;
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Join Range") {
			Debug.Log("in linking range");
			isInLinkingRange = true;
		}
	}

	void OnTriggerExit(Collider col) {
		if (col.gameObject.tag == "Join Range") {
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
		if (!isInLinkingRange) return;
		isLinked = true;	

		gameObject.GetComponent<Rigidbody>().mass = 0;
		//gameObject.GetComponent<PlayerController>().FreezePosition();
		//gameObject.GetComponent<BoxCollider>().isTrigger = true;
		// gameObject.GetComponent<Rigidbody>().isKinematic = true;

		if (GameController.currentState[GameState.SQUIRREL_ACTIVE])
			PlayerController.ToggleControl();
	}
	public void Unlink() {
		isLinked = false;
		gameObject.GetComponent<Rigidbody>().mass = startMass;
		//gameObject.GetComponent<PlayerController>().UnfreezePosition();
		// gameObject.GetComponent<Rigidbody>().isKinematic = false;
		//gameObject.GetComponent<BoxCollider>().isTrigger = false;
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
