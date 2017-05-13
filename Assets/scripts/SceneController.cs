using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SceneController : MonoBehaviour {
	
	public AtmosphereController atmosphereController;
	public TimeCycler timeCycler;
	public GameController gameController;

	// Use this for initialization

	void Start () {
		if (PlayerPrefs.GetInt("prototypeComplete") == 1) {
			atmosphereController.ChangeTimeOfDay(AtmosphereController.TimeOfDay.NIGHT, 0);
			gameController.musicFadeInTime = 1000;
		}
	}
}
