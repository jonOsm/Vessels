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
	private MobileJoystick mobileJoystick;
	
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();	
		mobileJoystick = FindObjectOfType<MobileJoystick>();	
		jumpIsTriggered = false;
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
	}

	bool CheckJumpInput() {
		if (Input.GetButton("Jump")) {
			return true;
		} 

		if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) {
			Debug.Log("Screen Touched AND begin phase!");
			return true;
		}	
		return false;

		// if (Input.touchCount > 0) {
		// 	if(Input.touches[0].phase == TouchPhase.Began) {
		// 		Debug.Log("Screen Touched AND begin phase!");
		// 		mobileJumpIsTriggered = true;
		// 	}	
		// 	if(Input.touches[0].phase == TouchPhase.Ended) {
		// 		Debug.Log("Screen Touched AND begin phase!");
		// 		mobileJumpIsCancelled = true;
		// 	}	
		// }
	}

	bool CheckJumpInputRelease() {
		if (Input.GetButtonUp("Jump")) {
			return true;
		}
		if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended) {
			return true;
		}	
		return false;
	}
	Vector3 CalculateRunVector() {
		if (Mathf.Abs(horizontalAxis) < 0.2) {
			horizontalAxis = 0;
		}

		if (Mathf.Abs(forwardAxis) < 0.2) {
			forwardAxis = 0;
		}

		if (isDirectionForced) {
			return Quaternion.Euler(0, 45, 0) * 
				new Vector3(Mathf.Abs(horizontalAxis*hSpeed) *-1, 0, 0 );
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
			mobileJoystick.mobileJumpIsTriggered = false;
		}

		if (isJumpCancelled) {
			if (rb.velocity.y > 0) {
				calculatedJumpVel= 0;
			}
			isJumpCancelled = false;
			mobileJoystick.mobileJumpIsCancelled = false;
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

