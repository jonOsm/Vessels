using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour {

	public bool playerNear = false;	
	Animator animator;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.GetComponent<Interactor>()) {
			playerNear = true;
			animator.SetBool("playerNear", true);	
		}
	}

	void OnTriggerExit(Collider col) {
		if (col.gameObject.GetComponent<Interactor>()) {
			playerNear = true;
			animator.SetBool("playerNear", false);	
		}
	}
}
