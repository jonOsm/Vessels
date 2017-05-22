using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feet : MonoBehaviour {
	public float groundedRaycastDistance;
	private PlayerController player;
	public bool grounded;
	// Use this for initialization
	void Start () {
		player = transform.parent.gameObject.GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		//TODO: FILTER ONLY LAYERS WE WANT TO BE ABLE TO JUMP FROM
		Debug.DrawRay(transform.position, Vector3.down*groundedRaycastDistance, Color.red, 0.5f, false);
		grounded = Physics.Raycast(transform.position, Vector3.down,
			groundedRaycastDistance);
		// if (grounded) {
		// 	player.currentJumps = 0;
		// }
	}

	void LateUpdate() {
		// if (grounded) {
		// 	player.currentMaxVerticalVel = player.maxVerticalWalkVel;
		// }
	}
}
