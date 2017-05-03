using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour {

	public Vector3 offset = new Vector3(0, 1.5f, 0);
	private Teleporter respawner;
	// Use this for initialization
	void Start () {
		respawner = GameObject.FindGameObjectWithTag("RespawnTrigger").GetComponent<Teleporter>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col) {
		if (col.GetComponent<PlayerController>()) {
			SetRespawnLocation();
		}
	}
	
	void OnCollisionEnter(Collision col) {
		if (col.gameObject.GetComponent<PlayerController>()) {
			SetRespawnLocation();
		}
	}

	void SetRespawnLocation() {
		respawner.targetLocation = gameObject;
		respawner.offset = offset; 
	}
}
