﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {

	public GameObject targetLocation;
	public Vector3 offset = Vector3.forward;
	public float removeControlTime = 0;

	private AudioSource audioSource;
		
	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col) {
		PlayerController player = col.gameObject.GetComponent<PlayerController>();
		if (player) {
			if (audioSource) {
				audioSource.Play();
			}
			player.transform.position = targetLocation.transform.position+offset;
			player.FreezePosition();
			player.UnfreezePosition(removeControlTime);
		}
	}
	
}
