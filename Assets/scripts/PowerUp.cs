using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {
	public enum PowerupType {
		MAXJUMPS, WALLJUMPING 
	}
	public PowerupType powerupType;
	PlayerController player;
	// Use this for initialization
	void Start () {
		
		player = GameObject.Find("Player").GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.GetComponent<Interactor>()) {
			switch (powerupType) {
				case PowerupType.MAXJUMPS:
					IncreaseMaxJumps();
					break;
				case PowerupType.WALLJUMPING:
					EnableWallJumping();
					break;

			}	
		}
	}

	void IncreaseMaxJumps() {
		player.maxJumps++;
		Disappear();
	}

	void EnableWallJumping() {
		player.wallJumpingEnabled = true;
		Disappear();
	}

	void Disappear() {
		gameObject.GetComponent<Collider>().enabled = false;
		gameObject.GetComponent<MeshRenderer>().enabled = false;
	}
}
