using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float hSpeed = 5f;
	public float fSpeed = 5f;
	public float jumpForce = 1000;
	public float jumpVel = 5;
	public float maxJumpTime = 0.75f;
	public float maxJumpHeight = 2;

	private Rigidbody rb;

	private float horizontalAxis;
	private float forwardAxis; 
	private float contextualMaxJumpHeight;

	private float jumpTimer;
	private bool jumpIsTriggered;
	private bool isJumpButtonDown;
	private bool isJumpCancelled;
	
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();	
		jumpIsTriggered = false;
	}
		
	// Update is called once per frame
	void Update () {
		horizontalAxis = Input.GetAxis("Horizontal");
		forwardAxis = Input.GetAxis("Forward");
		isJumpButtonDown = Input.GetButton("Jump");

		if (Input.GetButtonDown("Jump")) {
			//don't want to assign false here;
			jumpIsTriggered = true;
			float contextualMaxJumpHeight = rb.position.y + maxJumpHeight;

		}

		// if (Input.GetButtonUp("Jump")) {
		// 	isJumpCancelled = true;
		// }


	}

	void FixedUpdate() {

		Vector3 newVel = CalculateVelocity();
			//attempt to jump
		if (jumpIsTriggered) {
			// rb.useGravity = false;
			newVel.y=jumpVel;
			jumpIsTriggered = false;
		} else {
			// rb.useGravity = true;
		}

		// if (isJumpCancelled) {
		// 	newVel.y = 0;
		// }
		rb.velocity = newVel;
	}

	Vector3 CalculateVelocity() {
		if (Mathf.Abs(horizontalAxis) < 0.2) {
			horizontalAxis = 0;
		}

		if (Mathf.Abs(forwardAxis) < 0.2) {
			forwardAxis = 0;
		}

		return Quaternion.Euler(0, 45, 0) * 
			new Vector3(horizontalAxis*hSpeed, rb.velocity.y , forwardAxis*fSpeed);
	}

	void OnCollisionEnter(Collision col) {
		GameObject other = col.collider.gameObject;
		// layer 8 is "Ground" layer
		if (other.layer == 8) {
			jumpIsTriggered = false;
			isJumpCancelled = false;
			jumpTimer = 0;
			
		}
	}

	void OnCollisionExit(Collision col) {
		GameObject other = col.collider.gameObject;

		// layer 8 is "Ground" layer
		if (other.layer == 8) {
		}
	}
}
