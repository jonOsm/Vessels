using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcedMovementTrigger : MonoBehaviour {
	public float forwardSpeed;
	public float horizontalSpeed;
	public bool preventJumping;

	private PlayerController player;
	// Use this for initialization
	void Start () {
		player = FindObjectOfType<PlayerController>();	
	}
	
	// Update is called once per frame
	void OnTriggerEnter() {
		player.SlowMovement(forwardSpeed, horizontalSpeed);
		if (preventJumping) {
			player.DisableJumping();
		}
		// player.forceDirection();
	}
}
