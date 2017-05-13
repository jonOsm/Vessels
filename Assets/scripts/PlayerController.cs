using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float hSpeed = 5f;
	public float walkHSpeed = 2.5f;
	public float fSpeed = 5f;
	public float fwalkSpeed = 2.5f;
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

	private bool isDirectionForced;
	private VJHandler virtualJoystick;
	
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();	
		jumpIsTriggered = false;
		virtualJoystick = FindObjectOfType<VJHandler>();
	}
		
	// Update is called once per frame
	void Update () {
		horizontalAxis = Input.GetAxis("Horizontal");
		forwardAxis = Input.GetAxis("Forward");

		if (CheckJumpInput() && jumpingEnabled && currentJumps < maxJumps) {
			//don't want to assign false here;
			jumpIsTriggered = true;

		 }
		//  else if (Input.GetButtonDown("Jump") && jumpingEnabled && isAgainstWall) {
		// 	Debug.Log("JUMPING OFF OF WALL");	
		// }

		// if (Input.GetButtonUp("Jump") || mobileJoystick.mobileJumpIsCancelled) {
		// 	isJumpCancelled = true;
		// }
		if (CheckJumpInputRelease()) {
			isJumpCancelled = true;
		}
	}

	void FixedUpdate() {

		Vector3 newVel = CalculateRunVector();
		newVel.y = CalculateJumpVelocity();
		rb.velocity = newVel;
		transform.rotation = CalculateRotation();
	}

	Quaternion CalculateRotation() {
		if (Mathf.Abs(rb.velocity.x) > 0 || Mathf.Abs(rb.velocity.z) > 0) {
			return Quaternion.LookRotation(new Vector3(rb.velocity.x, 0, rb.velocity.z));
		}

		return transform.rotation;
	}
	bool CheckJumpInput() {
		if (Input.GetButtonDown("Jump")) {
			return true;
		} 

		// if(Input.touchCount > 0 && 
		// 	Input.touches[0].phase == TouchPhase.Began &&
		// 	Input.touches[0].position.x >= Screen.width/2) {
		// }	

		foreach (Touch touch in Input.touches) {
			if (touch.phase == TouchPhase.Began &&
				touch.position.x >= Screen.width*0.5f &&
				touch.position.y < Screen.height*0.8f) {
				return true;
			}
		}
		return false;
	}

	bool CheckJumpInputRelease() {
		if (Input.GetButtonUp("Jump")) {
			return true;
		}
		if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended) {
			return true;
		}
		foreach (Touch touch in Input.touches) {
			if (touch.phase == TouchPhase.Ended &&
				touch.position.x >= Screen.width*0.5f &&
				touch.position.y < Screen.height*0.8f) {
				return true;
			}
		}	
		return false;
	}
	Vector3 CalculateRunVector() {
		float hAxis = horizontalAxis;	
		float fAxis = forwardAxis;	

		if (virtualJoystick) {
			hAxis = virtualJoystick.InputDirection.x*-1;
			fAxis = virtualJoystick.InputDirection.y*-1;
		}
		if (Mathf.Abs(hAxis) < 0.2f) {
			hAxis = 0;
		}

		if (Mathf.Abs(fAxis) < 0.2f) {
			fAxis = 0;
		}

		if (isDirectionForced) {
			return Quaternion.Euler(0, 45, 0) * 
				new Vector3(Mathf.Abs(hAxis*hSpeed) *-1, 0, 0 );
		}
		return Quaternion.Euler(0, 45, 0) * 
			new Vector3(hAxis*hSpeed, 0, fAxis*fSpeed);
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
		if (GameController.currentState[GameState.PLAYER_FROZEN] ||
			GameController.currentState[GameState.MENU_OPEN]) {
				return;
			}
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

	public void SlowMovement(float horizontalSpeed, float forwardSpeed) {
		hSpeed = horizontalSpeed;
		fSpeed = forwardSpeed;
	}

	public void forceDirection() {
		isDirectionForced = true;
	}
}

