using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileJoystick : MonoBehaviour {

	// Use this for initialization
	public bool mobileJumpIsTriggered = false;
	public bool mobileJumpIsCancelled = false;
	private PlayerController player;
	void Start () {
		 player = FindObjectOfType<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0) {
			if(Input.touches[0].phase == TouchPhase.Stationary) {
				Debug.Log("Screen Touched AND begin phase!");
				mobileJumpIsTriggered = true;
			}	
			if(Input.touches[0].phase == TouchPhase.Ended) {
				Debug.Log("Screen Touched AND begin phase!");
				mobileJumpIsCancelled = true;
			}	
		}
	}
}
