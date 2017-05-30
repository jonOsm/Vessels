using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour, IActivatable {

	public bool playerNear = false;	
	public bool isLocked = false;
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
			if (isLocked) return;
			playerNear = true;
			animator.SetBool("playerNear", true);	
		}
	}

	void OnTriggerExit(Collider col) {
		if (col.gameObject.GetComponent<Interactor>()) {
			if (isLocked) return;
			playerNear = true;
			animator.SetBool("playerNear", false);	
		}
	}

	void Unlock() {
		//do something
		isLocked = false;
	}

	public void Activate()  {
		Unlock();
	}

	public void Deactivate()  {

	}
}
