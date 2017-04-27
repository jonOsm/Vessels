using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public bool lockVerticalMotion = false;
	public float verticalMoveTime;
	public float horizontalMoveTime;
	public float forwardMoveTime;

	private GameObject player;	
	private Vector3 cameraOffset;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<PlayerController>().gameObject;
		cameraOffset = CalculateCameraOffset();
	}
	
	// Update is called once per frame
	void Update () {

	}
	void FixedUpdate() {
		Vector3 newCameraPos = player.transform.position - cameraOffset;
		Vector3 playerPos = player.transform.position;

		//IMPORTANT: that this is NOT the way the unity docs uses lerp, in this case the camera position is acting as interpolator
		newCameraPos.y = Mathf.Lerp(transform.position.y, playerPos.y-cameraOffset.y, verticalMoveTime*Time.fixedDeltaTime);
		newCameraPos.x = Mathf.Lerp(transform.position.x, playerPos.x-cameraOffset.x, horizontalMoveTime*Time.fixedDeltaTime);
		newCameraPos.z = Mathf.Lerp(transform.position.z, playerPos.z-cameraOffset.z, forwardMoveTime*Time.fixedDeltaTime);

		//note that this approach doesn't have as good a feel as lerp, but makes more sense semantically
		//newCameraPos.y = Mathf.MoveTowards(transform.position.y, player.transform.position.y-cameraOffset.y, verticalMoveTime);

		gameObject.transform.position = newCameraPos;
	}

	Vector3 CalculateCameraOffset() {
		return player.transform.position - transform.position;
	}
}
