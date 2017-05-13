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
	public bool activatorIsOff = true;
	public Material onMaterial;
	public Material offMaterial;

	private IActivatable activatable;
	private int timesActivated = 0;
	private MeshRenderer meshRenderer;

	// Use this for initialization
	void Start () {
		 activatable = activatableGameObject.GetComponent<IActivatable>();
		 meshRenderer = GetComponent<MeshRenderer>();
		 if (!activatorIsOff) {
			 UpdateMaterial();
		 }
	}
	

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.GetComponent<Interactor>()) {
			ActivationSwitchboard();
		}
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

		UpdateMaterial();
	}	

	void SingleUseActivation() {
		if (timesActivated > 0) return;
		if (activatorIsOff) {
			ActivateActivatable();
			// GetComponent<Interaction>().enabled = false;
		} 
	}
	void ToggleActivation() {
		if (activatorIsOff) {
			ActivateActivatable();
		} else {
			DeactivateActivatable();
		} 
	}

	void ActivateActivatable() {
		activatorIsOff = false;
		activatable.Activate();
		timesActivated++;
	}
	void DeactivateActivatable() {
		activatorIsOff = true;
		activatable.Deactivate();
		timesActivated++;
	}

	void UpdateMaterial() {
		if (activatorIsOff) {
			meshRenderer.material = offMaterial;
		} else {
			meshRenderer.material = onMaterial;
		}
	}
}
