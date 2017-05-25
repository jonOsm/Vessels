using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleOutline : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnBecameVisible() {
		Debug.Log("visible");
	}

	void OnBecameInvisible() {
		Debug.Log("invisible");
	}
}
