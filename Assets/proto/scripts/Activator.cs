using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour {
	public enum ActivatorType {
		SINGLEUSE,
		TOGGLE,
		MULTIUSE
	}
	public GameObject activatableGameObject;
	public ActivatorType activatorType = ActivatorType.SINGLEUSE;
	public bool isActive = true;

	private IActivatable activatable;
	private int timesActivated = 0;

	// Use this for initialization
	void Start () {
		 activatable = activatableGameObject.GetComponent<IActivatable>();
	}
	

	void OnCollisionEnter(Collision col) {
		ActivationSwitchboard();
	}
	void ActivationSwitchboard() {
		switch(activatorType) {
			case ActivatorType.SINGLEUSE:
				SingleUseActivation();
				break;
			case ActivatorType.TOGGLE:
				ToggleActivation();
				break;
			case ActivatorType.MULTIUSE:
			default:
				break;
		}
	}	

	void SingleUseActivation() {
		if (timesActivated > 0) return;
		if (isActive) {
			ActivateActivatable();
			GetComponent<Interaction>().enabled = false;
		} 
	}
	void ToggleActivation() {
		if (isActive) {
			ActivateActivatable();
		} else {
			DeactivateActivatable();
		} 
	}

	void ActivateActivatable() {
		isActive = false;
		activatable.Activate();
		timesActivated++;
	}
	void DeactivateActivatable() {
		isActive = true;
		activatable.Deactivate();
		timesActivated++;
	}

}
