using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtmosphereTrigger : MonoBehaviour {

	private AtmosphereController atmosphereController;
	public AtmosphereController.TimeOfDay newTimeOfDay;
	// Use this for initialization
	void Start () {
		atmosphereController = FindObjectOfType<AtmosphereController>();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter() {
		atmosphereController.ChangeTimeOfDay(newTimeOfDay);
	}
}
