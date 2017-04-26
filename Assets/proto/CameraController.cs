using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public bool lockVerticalMotion = false;
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

		if (lockVerticalMotion) {
			newCameraPos.y = transform.position.y; 
		}

		gameObject.transform.position = newCameraPos;
	}

	Vector3 CalculateCameraOffset() {
		return player.transform.position - transform.position;
	}
}
