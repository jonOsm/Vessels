﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJump : MonoBehaviour {

	private PlayerController player;
	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player").GetComponent<PlayerController>();	
	}

	void OnTriggerEnter() {
		if (player.wallJumpingEnabled) {
			player.currentJumps = 0;	
		}
	}
}
