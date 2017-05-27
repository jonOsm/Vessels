using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class CameraController : MonoBehaviour {
	public bool lockCameraPos;	
	public float verticalMoveTime;
	public float horizontalMoveTime;
	public float forwardMoveTime;
	public bool useDefaultOffset;
	public float zoomStep;
	public float rotationSpeed;
	public LayerMask playerVisibleMask;

	private GameObject player;	
	private Vector3 cameraOffset;
	private Vector3 defaultCameraOffset = new Vector3(-41.4f, -27.5f, -41.4f);
	private float rotationInputFactor;
	private float peakHorizontalInputFactor;
	private float interpolationMultiplier = 1;
	private Camera theCamera;

	// Use this for initialization
	void Start () {
		player = PlayerController.controlledModel;
		theCamera = GetComponent<Camera>();
		if (useDefaultOffset) {
			cameraOffset = defaultCameraOffset; 
		}
		else {
			cameraOffset = CalculateCameraOffset();
		}
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		Debug.DrawLine(transform.position, player.transform.position, Color.green, 0, false);
		var playerInView = Physics.Linecast(this.transform.position, player.transform.position, out hit, playerVisibleMask);
		if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Player")) {
			player.GetComponentInChildren<Outline>().eraseRenderer = true;
		} else {
			player.GetComponentInChildren<Outline>().eraseRenderer = false;
		}
	}
	void FixedUpdate() {
		if (lockCameraPos) return;

		gameObject.transform.position = InterpolatePosition();
		if (Mathf.Abs(rotationInputFactor) > 0) {
			transform.Rotate(new Vector3(0,rotationInputFactor*rotationSpeed, 0) ,Space.World);
			cameraOffset = Quaternion.Euler(0, rotationInputFactor*rotationSpeed, 0) * cameraOffset;
			interpolationMultiplier = 6;
		}
		else {
			interpolationMultiplier = 1;
		}
	}

	Vector3 CalculateCameraOffset() {
		return player.transform.position - transform.position;

	}

	Vector3 InterpolatePosition() {
		Vector3 newCameraPos = new Vector3();
		Vector3 playerPos = player.transform.position;

		//IMPORTANT: that this is NOT the way the unity docs uses lerp, in this case the camera position is acting as interpolator
			newCameraPos.x = Mathf.Lerp(transform.position.x, playerPos.x-cameraOffset.x, horizontalMoveTime*Time.fixedDeltaTime*interpolationMultiplier);
			newCameraPos.y = Mathf.Lerp(transform.position.y, playerPos.y-cameraOffset.y, verticalMoveTime*Time.fixedDeltaTime*interpolationMultiplier);
			newCameraPos.z = Mathf.Lerp(transform.position.z, playerPos.z-cameraOffset.z, forwardMoveTime*Time.fixedDeltaTime*interpolationMultiplier);
		//note: you can fairly easily add dead zone by stopping interpolation if within a certain range
		//note: you can snap to middle if within a certain range as well
		

		//interesting effect, pulls camera away from center so you can peak at the left and right areas of the map
		// if (Mathf.Abs(peakHorizontalInputFactor) > 0) {
		// 	transform.RotateAround(player.transform.position, Vector3.up, peakHorizontalInputFactor);
		// 	return Quaternion.Euler(0, rotationInputFactor, 0) * newCameraPos;
		// }
		return newCameraPos;

	}

	public void setObjectToFocus(GameObject objectToFocus) {
		player = objectToFocus;
	}

	public void ZoomIn() {
		cameraOffset = Vector3.MoveTowards(cameraOffset, Vector3.zero, zoomStep);

	}

	public void ZoomOut() {
		cameraOffset = Vector3.MoveTowards(cameraOffset, Vector3.zero, -zoomStep);
	}

	public void RotateAroundFocus(float inputFactor) {
		rotationInputFactor = inputFactor;
	}

	public void PeakHorizontal(float inputFactor) {
		peakHorizontalInputFactor = inputFactor;
	}

	
}
