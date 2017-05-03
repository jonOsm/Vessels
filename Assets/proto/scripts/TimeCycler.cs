using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCycler : MonoBehaviour {

	public float timeOfDayTransitionTime = 60;

	public AtmosphereController.TimeOfDay[] timesToCycle;
	public bool isEnabled;
	private float timeSinceStartOfTransition = 0;
	private int currentTimeIndex = 0;
	private AtmosphereController atmosphereController;

	// Use this for initialization
	void Start () {
		atmosphereController = FindObjectOfType<AtmosphereController>();
		if (isEnabled) {
			TransitionToNextTime();
		}
	}
	
	// Update is called once per frame
	void Update () {
		timeSinceStartOfTransition += Time.deltaTime;
		if (isEnabled && timeSinceStartOfTransition > timeOfDayTransitionTime) {
			TransitionToNextTime();
		}

	}

	void TransitionToNextTime() {
		timeSinceStartOfTransition = 0;
		atmosphereController.ChangeTimeOfDay(timesToCycle[currentTimeIndex], timeOfDayTransitionTime);	
		currentTimeIndex++;
		if (currentTimeIndex >= timesToCycle.Length) {
			currentTimeIndex = 0;
		}
	}

	public void StopCycling() {
		isEnabled = false;
	}

	public void StartCycling() {
		isEnabled = true;
	}
}
