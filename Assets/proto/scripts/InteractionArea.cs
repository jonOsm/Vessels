using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionArea : MonoBehaviour {
	private bool inInspectRange = false;
	private GameObject objectToInspect;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown("Inspect")) {
			if (objectToInspect)
				Inspect(objectToInspect);
		}
	}

	void OnTriggerStay(Collider col) {
		inInspectRange = true;
		objectToInspect = col.gameObject;
	}

	void OnTriggerExit(Collider col) {
		inInspectRange = false;
		objectToInspect = null;
	}

	void Inspect(GameObject go) {
		Interaction interaction = go.GetComponent<Interaction>(); 
		if (interaction) {
			interaction.Interact();
		}
	}
}
