using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCycler : MonoBehaviour {

	public bool isActivated = true;
	public float timeOfDayTransitionTime = 60;

	public AtmosphereController.TimeOfDay[] timesToCycle;

	private float timeSinceStartOfTransition = 0;
	private int currentTimeIndex = 0;
	private AtmosphereController atmosphereController;

	// Use this for initialization
	void Start () {
		atmosphereController = FindObjectOfType<AtmosphereController>();
		TransitionToNextTime();
	}
	
	// Update is called once per frame
	void Update () {
		timeSinceStartOfTransition += Time.deltaTime;
		if (timeSinceStartOfTransition > timeOfDayTransitionTime) {
			TransitionToNextTime();
		}

	}

	void TransitionToNextTime() {
		Debug.Log("running?");
		timeSinceStartOfTransition = 0;
		atmosphereController.ChangeTimeOfDay(timesToCycle[currentTimeIndex], timeOfDayTransitionTime);	
		currentTimeIndex++;
		if (currentTimeIndex >= timesToCycle.Length) {
			currentTimeIndex = 0;
		}
	}
}
