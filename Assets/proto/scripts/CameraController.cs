using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public bool lockVerticalMotion = false;
	public float verticalMoveThreshold;
	public float verticalMoveTime;

	private GameObject player;	
	private Vector3 cameraOffset;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<PlayerController>().gameObject;
		cameraOffset = CalculateCameraOffset();
		
		print("player.transform.position.y: " + player.transform.position.y);
		print ("cameraOffset.y: "+ cameraOffset.y);

	}
	
	// Update is called once per frame
	void Update () {

	}
	void FixedUpdate() {
		Vector3 newCameraPos = player.transform.position - cameraOffset;

		newCameraPos.y = Mathf.Lerp(transform.position.y, player.transform.position.y-cameraOffset.y, verticalMoveTime*Time.fixedDeltaTime);

		gameObject.transform.position = newCameraPos;
	}

	Vector3 CalculateCameraOffset() {
		return player.transform.position - transform.position;
	}
}
