using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtmosphereTrigger : MonoBehaviour {

	public AtmosphereController.TimeOfDay newTimeOfDay;
	public float transitionDuration;
	public bool stopTimeCycle = true;

	private AtmosphereController atmosphereController;
	private TimeCycler timeCycler;
	// Use this for initialization
	void Start () {
		atmosphereController = FindObjectOfType<AtmosphereController>();	
		timeCycler = FindObjectOfType<TimeCycler>();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter() {
		if (stopTimeCycle) {
			timeCycler.StopCycling();
		}
		atmosphereController.ChangeTimeOfDay(newTimeOfDay, transitionDuration);
	}
}
