using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float hSpeed = 5f;
	public float fSpeed = 5f;
	public float jumpVel = 5;
	public int maxJumps = 1;
	public bool wallJumpingEnabled = false;

	[HideInInspector]
	public int currentJumps = 0;

	private Rigidbody rb;

	private float horizontalAxis;
	private float forwardAxis; 

	private bool jumpingEnabled = true;
	private bool jumpIsTriggered;
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

		if (Input.GetButtonDown("Jump") && jumpingEnabled && currentJumps < maxJumps) {
			//don't want to assign false here;
			jumpIsTriggered = true;

		 }
		//  else if (Input.GetButtonDown("Jump") && jumpingEnabled && isAgainstWall) {
		// 	Debug.Log("JUMPING OFF OF WALL");	
		// }

		if (Input.GetButtonUp("Jump")) {
			isJumpCancelled = true;
		}
	}

	void FixedUpdate() {

		Vector3 newVel = CalculateRunVector();
		newVel.y = CalculateJumpVelocity();
		rb.velocity = newVel;
	}

	Vector3 CalculateRunVector() {
		if (Mathf.Abs(horizontalAxis) < 0.2) {
			horizontalAxis = 0;
		}

		if (Mathf.Abs(forwardAxis) < 0.2) {
			forwardAxis = 0;
		}

		return Quaternion.Euler(0, 45, 0) * 
			new Vector3(horizontalAxis*hSpeed, 0, forwardAxis*fSpeed);
	}

	float CalculateJumpVelocity() {
		float calculatedJumpVel = rb.velocity.y;

		if (jumpIsTriggered) {
			// rb.useGravity = false;
			calculatedJumpVel=jumpVel;
			currentJumps++;
			jumpIsTriggered = false;
		}

		if (isJumpCancelled) {
			if (rb.velocity.y > 0) {
				calculatedJumpVel= 0;
			}
			isJumpCancelled = false;
		}
		return calculatedJumpVel;
	}

	public void FreezePosition() {
		rb.constraints |= RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
		DisableJumping();	
	}

	public void UnfreezePosition() {
		rb.constraints = RigidbodyConstraints.None;
		rb.constraints = RigidbodyConstraints.FreezeRotation;
		EnableJumping();
	}

	public void UnfreezePosition(float delay) {
		Invoke("UnfreezePosition", delay);
	}

	public void DisableJumping() {
		jumpingEnabled = false;
	}

	public void EnableJumping() {
		jumpingEnabled = true;	
	}
}
