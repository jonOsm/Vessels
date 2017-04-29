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
	public bool enabled = true;

	private IActivatable activatable;
	private int timesActivated = 0;

	// Use this for initialization
	void Start () {
		 activatable = activatableGameObject.GetComponent<IActivatable>();
	}
	
	// Update is called once per frame
	void Update () {
		if (activatorType == ActivatorType.SINGLEUSE && timesActivated > 0)	{
			Disable();
		}

	}

	void OnCollisionEnter(Collision col) {
		if (enabled) {
			activatable.Activate();
			timesActivated++;
		}

	}
	
	void Disable() {
		enabled = false;
	}

}
