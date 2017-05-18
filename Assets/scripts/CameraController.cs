using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public bool lockVerticalMotion = false;
	public float verticalMoveTime;
	public float horizontalMoveTime;
	public float forwardMoveTime;
	public bool useDefaultOffset;

	private GameObject player;	
	private Vector3 cameraOffset;
	private Vector3 defaultCameraOffset = new Vector3(-41.4f, -27.5f, -41.4f);

	// Use this for initialization
	void Start () {
		player = PlayerController.controlledModel;
		if (useDefaultOffset) {
			cameraOffset = defaultCameraOffset; 
		}
		else {
			Debug.Log("calculating_default");
			cameraOffset = CalculateCameraOffset();
		}
	}
	
	// Update is called once per frame
	void Update () {

	}
	void FixedUpdate() {
			gameObject.transform.position = InterpolatePosition();
	}

	Vector3 CalculateCameraOffset() {
		return player.transform.position - transform.position;

	}

	Vector3 InterpolatePosition() {
		Vector3 newCameraPos = new Vector3();
		Vector3 playerPos = player.transform.position;

		//IMPORTANT: that this is NOT the way the unity docs uses lerp, in this case the camera position is acting as interpolator

		newCameraPos.y = Mathf.Lerp(transform.position.y, playerPos.y-cameraOffset.y, verticalMoveTime*Time.fixedDeltaTime);
		newCameraPos.x = Mathf.Lerp(transform.position.x, playerPos.x-cameraOffset.x, horizontalMoveTime*Time.fixedDeltaTime);
		newCameraPos.z = Mathf.Lerp(transform.position.z, playerPos.z-cameraOffset.z, forwardMoveTime*Time.fixedDeltaTime);
		//note: you can fairly easily add dead zone by stopping interpolation if within a certain range
		//note: you can snap to middle if within a certain range as well
		return newCameraPos;

	}

	public void setObjectToFocus(GameObject objectToFocus) {
		player = objectToFocus;
	}
}
