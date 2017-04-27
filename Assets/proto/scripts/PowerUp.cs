using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col) {
		PlayerController player = col.gameObject.GetComponent<PlayerController>();
		if (player) {
			player.maxJumps++;
			Destroy(gameObject);
		}
	}
}
