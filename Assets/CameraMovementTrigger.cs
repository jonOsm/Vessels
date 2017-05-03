using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementTrigger : MonoBehaviour {
	public GameObject objectToTrack;
	public float verticalMoveTime;
	public float horizontalMoveTime;
	public float forwardMoveTime;

	private CameraController mainCamera;
	// Use this for initialization
	void Start () {
		mainCamera = FindObjectOfType<CameraController>();
	}
	
	// Update is called once per frame
	void OnTriggerEnter () {
		mainCamera.verticalMoveTime = verticalMoveTime; 
		mainCamera.horizontalMoveTime = horizontalMoveTime;
		mainCamera.forwardMoveTime = forwardMoveTime;
		mainCamera.setObjectToFocus(objectToTrack);
	}
}
