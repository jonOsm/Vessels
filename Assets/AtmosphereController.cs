using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtmosphereController : MonoBehaviour {

	public enum TimeOfDay {
		DAY, EVENING, NIGHT
	}

	public Camera mainCamera;
	public Light mainLightSource;
	public TimeOfDay defaultTimeOfDay = TimeOfDay.DAY;
	public float defaultTransitionTime = 2f;

	public Color daySkyColor;
	public Color eveningSkyColor;
	public Color nightSkyColor;

	public float daySkyLightingIntensity;
	public float eveningSkyLightingIntensity;
	public float nightSkyLightingIntensity;

	public float daySkyLightingColor;
	public float eveningSkyLightingColor;
	public float nightSkyLightingColor;

	public TimeOfDay currentTimeofDay;

	private Color transitionStartBackgroundColor; 
	private float transitionStartLightIntensity; 
	private float transitionStartAmbientIntensity; 
	private TimeOfDay previousTimeOfDay;
	private bool isTransitioning = false;
	private float transitionDuration = 0;
	private float transitionProgress = 0;


	// Use this for initialization
	void Start () {
		currentTimeofDay = defaultTimeOfDay;
		// ChangeTimeOfDay(currentTimeofDay);
	}
	
	// Update is called once per frame
	void Update () {
		if (isTransitioning) {
			ApplyChanges();
		}	
	}

	public void ChangeTimeOfDay(TimeOfDay timeOfDay) {
		ChangeTimeOfDay(timeOfDay, defaultTransitionTime);
	}

	public void ChangeTimeOfDay(TimeOfDay timeOfDay, float transitionDuration) {
		transitionProgress = 0;
		isTransitioning = true;
		this.transitionDuration = transitionDuration;	
		previousTimeOfDay = currentTimeofDay;
		currentTimeofDay = timeOfDay;
		transitionStartBackgroundColor = mainCamera.backgroundColor;
		transitionStartLightIntensity = mainLightSource.intensity;
		transitionStartAmbientIntensity = RenderSettings.ambientIntensity;
	}


	public void ApplyChanges() {
		transitionProgress += Time.deltaTime/transitionDuration;
		//Debug.Log(transitionProgress);
		switch(currentTimeofDay) {
			case TimeOfDay.DAY:
				mainCamera.backgroundColor = Color.Lerp(transitionStartBackgroundColor, daySkyColor, transitionProgress);
				mainLightSource.intensity = Mathf.Lerp(transitionStartLightIntensity, daySkyLightingIntensity, transitionProgress);
				break;
			case TimeOfDay.EVENING:
				mainCamera.backgroundColor = Color.Lerp(transitionStartBackgroundColor, eveningSkyColor, transitionProgress);
				mainLightSource.intensity = Mathf.Lerp(transitionStartLightIntensity, eveningSkyLightingIntensity, transitionProgress);
				break;
			case TimeOfDay.NIGHT:
				mainCamera.backgroundColor = Color.Lerp(transitionStartBackgroundColor, nightSkyColor, transitionProgress);
				mainLightSource.intensity = Mathf.Lerp(transitionStartLightIntensity, nightSkyLightingIntensity, transitionProgress);

				RenderSettings.ambientIntensity = Mathf.Lerp(transitionStartAmbientIntensity, 0, transitionProgress);
				break;
		}

		if (transitionProgress >= 1) {
			isTransitioning = false;
			transitionProgress = 0;
		}
	}
	
	// public Color GetColorByDay(TimeOfDay timeOfDay) {

	// 	switch(timeOfDay) {
	// 		case TimeOfDay.DAY:
	// 			return daySkyColor;
	// 		case TimeOfDay.EVENING:
	// 			return eveningSkyColor;
	// 		case TimeOfDay.NIGHT:
	// 			return nightSkyColor;
	// 		default:
	// 			return Color.white;
	// 	}
	// }

}
