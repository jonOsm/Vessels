using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feet : MonoBehaviour {
	public float groundedRaycastDistance;

	private PlayerController player;
	private bool grounded;
	// Use this for initialization
	void Start () {
		player = transform.parent.gameObject.GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		grounded = Physics.Raycast(transform.position, Vector3.down, groundedRaycastDistance);
		print(grounded);
		if (grounded) {
			player.currentJumps = 0;
		}
	}

	// void OnCollisionEnter(Collision col) {
	// 	Debug.Log("collision");	
	// 	GameObject other = col.collider.gameObject;
		
	// 	// layer 8 is "Ground" layer
	// 	if (other.layer == 8) {
	// 		player.currentJumps = 0;
	// 	}
	// }
	// void OnTriggerEnter(Collider col) {
	// 	Debug.Log("collision");	
	// 	GameObject other = col.gameObject;
		
	// 	// layer 8 is "Ground" layer
	// 	if (other.layer == 8) {
	// 		player.currentJumps = 0;
	// 	}
	// }
}
