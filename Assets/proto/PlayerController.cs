using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float hSpeed = 5f;
	public float fSpeed = 5f;
	public float jumpForce = 1000;
	public float maxJumpTime = 0.75f;
	public float maxJumpHeight = 2;

	private Rigidbody rb;

	private float horizontalAxis;
	private float forwardAxis; 

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

		if (Input.GetButtonDown("Jump")) {
			//don't want to assign false here;
			jumpIsTriggered = true;
		}

		if (Input.GetButtonUp("Jump")) {
			isJumpCancelled = true;
		}

		isJumpButtonDown = Input.GetButton("Jump");

	}

	void FixedUpdate() {

		rb.velocity = CalculateVelocity();
			//attempt to jump
		if (isJumpButtonDown &&
			jumpIsTriggered &&
			!isJumpCancelled &&
			jumpTimer <= maxJumpTime
			) {
			rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Force);
			jumpTimer += Time.fixedDeltaTime;
		}
	}

	Vector3 CalculateVelocity() {
		if (Mathf.Abs(horizontalAxis) < 0.2) {
			horizontalAxis = 0;
		}

		if (Mathf.Abs(forwardAxis) < 0.2) {
			forwardAxis = 0;
		}

		return Quaternion.Euler(0, 45, 0) * 
			new Vector3(horizontalAxis*hSpeed, 0 , forwardAxis*fSpeed);
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
