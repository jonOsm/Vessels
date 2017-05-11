using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsText : MonoBehaviour {

	public float timeToDisplay = 15;	
	// Use this for initialization
	void Start () {
		Invoke("Die", timeToDisplay);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Cancel")) {
			Die();
		};
	}

	void Die() {
		Destroy(gameObject);	
	}
}
