﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public static GameObject controlledModel;
	public static PlayerRobot playerRobot;
	public static PlayerSquirrel playerSquirrel;

	public float hSpeed = 5f;
	public float walkHSpeed = 2.5f;
	public float fSpeed = 5f;
	public float fwalkSpeed = 2.5f;
	public float maxVerticalWalkVel = 4;
	public float jumpVel = 5;
	public int maxJumps = 1;
	public bool wallJumpingEnabled = false;
	public bool isControlledModel = false;
	public bool	inFreeCameraMode;

	
	public int currentJumps = 0;

	[HideInInspector]
	public float currentMaxVerticalVel;

	private Rigidbody rb;

	private float horizontalAxis;
	private float forwardAxis; 
	private float scrollAxis; 
	private float rotationAxis; 

	private bool jumpingEnabled = true;
	private bool jumpIsTriggered;
	private bool isJumpCancelled;

	private bool isDirectionForced;
	private VJHandler virtualJoystick;

	private static CameraController theCamera;
	private GameController gameController;
	private Feet feet;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();	
		jumpIsTriggered = false;
		virtualJoystick = FindObjectOfType<VJHandler>();
		playerRobot = FindObjectOfType<PlayerRobot>();
		playerSquirrel = FindObjectOfType<PlayerSquirrel>();
		theCamera = FindObjectOfType<CameraController>();
		gameController = FindObjectOfType<GameController>();
		feet = GetComponentInChildren<Feet>();

		currentMaxVerticalVel = maxVerticalWalkVel;

		if (isControlledModel) {
			controlledModel = gameObject;
		} else {
			enabled = false;
			GetComponent<AudioListener>().enabled = false;
		}
	}

	// Update is called once per frame
	void Update () {
		horizontalAxis = Input.GetAxis("Horizontal");
		forwardAxis = Input.GetAxis("Forward");
		scrollAxis = Input.GetAxis("Mouse ScrollWheel");
		rotationAxis = Input.GetAxis("Camera Rotation");
		bool swapControlButtonDown = Input.GetButtonDown("Swap Control");
		bool linkControlButtonDown = Input.GetButtonDown("Link Control");

		if (feet.grounded && rb.velocity.y <=0) {
				currentJumps = 0;
				currentMaxVerticalVel = maxVerticalWalkVel;
		}

		if (CheckJumpInput() && jumpingEnabled && currentJumps < maxJumps) {
			//don't want to assign false here;
			jumpIsTriggered = true;

		 }
	
		if (CheckJumpInputRelease()) {
			isJumpCancelled = true;
		}

		if (Input.GetKeyDown(KeyCode.I)) {
			ToggleControl();
		}

		if(scrollAxis > 0) {
			theCamera.ZoomIn();
		} else if (scrollAxis < 0) {
			theCamera.ZoomOut();
		}

		if (swapControlButtonDown) {
			gameController.TogglePlayerModel();
		}

		if (linkControlButtonDown) {
			playerSquirrel.Link();
		}


		
		theCamera.RotateAroundFocus(rotationAxis);
	}

	void FixedUpdate() {
		Vector3 newVel = CalculateRunVector();
		
		newVel.y = CalculateJumpVelocity();
		//newVel.y = newVel.y > currentMaxVerticalVel ? currentMaxVerticalVel : newVel.y; 
		newVel.y = Mathf.Min(newVel.y, currentMaxVerticalVel);

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
		float hAxis = 0;
		float fAxis = 0;

		if (virtualJoystick) {
			hAxis = virtualJoystick.InputDirection.x*-1;
			fAxis = virtualJoystick.InputDirection.y*-1;
		}

		if (Mathf.Abs(horizontalAxis) > 0.2) hAxis = horizontalAxis;
		if (Mathf.Abs(forwardAxis) > 0.2) fAxis = forwardAxis;

		if (Mathf.Abs(hAxis) < 0.2f) {
			hAxis = 0;
		}

		if (Mathf.Abs(fAxis) < 0.2f) {
			fAxis = 0;
		}
		if (inFreeCameraMode) {

			//TODO: UNDERSTAND HOW THIS WORKS BECAUSE HONESTLY I HAVE NO IDEA I JUST GOT LUCKY
			Vector3 rawVector = new Vector3(hAxis*hSpeed, 0, fAxis*fSpeed);
			Quaternion q = theCamera.transform.rotation;
			return Quaternion.Euler(0, q.eulerAngles.y, 0) * rawVector*-1;
			//return theCamera.transform.TransformDirection(-rawVector);
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
				
			calculatedJumpVel=jumpVel;
			currentJumps++;
			//Debug.Break();
			jumpIsTriggered = false;
			currentMaxVerticalVel = jumpVel;
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

	public static void ToggleControl() {
		controlledModel.GetComponent<PlayerController>().enabled = false;
		controlledModel.GetComponent<AudioListener>().enabled = false;
		controlledModel.GetComponent<PlayerController>().FreezePosition();

		if (controlledModel.GetComponent<PlayerRobot>()) {
			playerSquirrel.Unlink();
			GameController.currentState[GameState.ROBOT_ACTIVE] = false;
			GameController.currentState[GameState.SQUIRREL_ACTIVE] = true;
			controlledModel = playerSquirrel.gameObject;
		}
		else {
			GameController.currentState[GameState.ROBOT_ACTIVE] = true;
			GameController.currentState[GameState.SQUIRREL_ACTIVE] = false;
			controlledModel = playerRobot.gameObject;

		}

		theCamera.setObjectToFocus(controlledModel);
		controlledModel.GetComponent<PlayerController>().UnfreezePosition();
		controlledModel.GetComponent<PlayerController>().enabled = true;
		controlledModel.GetComponent<AudioListener>().enabled = true;
	}

}

