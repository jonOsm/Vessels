using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour {
	void OnTriggerEnter(Collider col) {
		InteractionController ic = col.GetComponent<InteractionController>();	
		if (!ic) return;

		ic.PrimeInteraction();
	}
}
