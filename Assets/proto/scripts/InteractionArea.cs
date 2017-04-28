using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionArea : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerStay(Collider col) {
		if (Input.GetButtonDown("Inspect")) {
			Inspect(col.gameObject);
		}
	}


	void Inspect(GameObject go) {
		Interaction interaction = go.GetComponent<Interaction>(); 
		if (interaction) {
			interaction.Interact();
		}
	}
}
